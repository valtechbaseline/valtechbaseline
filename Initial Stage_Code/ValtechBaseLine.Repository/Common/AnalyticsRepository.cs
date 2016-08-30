using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Glass.Mapper;
using Sitecore.Analytics;
using Sitecore.Analytics.Data;
using Sitecore.Analytics.Data.Items;
using Sitecore.Analytics.DataAccess;
using Sitecore.Analytics.Model;
using Sitecore.Analytics.Model.Entities;
using Sitecore.Analytics.Tracking;
using Sitecore.Analytics.Tracking.SharedSessionState;
using Sitecore.Data;
using Sitecore.Diagnostics;
using Sitecore.ListManagement;
using Sitecore.ListManagement.ContentSearch.Model;
using Sitecore.ListManagement.ContentSearch.Pipelines.Locking;
using Sitecore.Modules.EmailCampaign;
using Sitecore.Modules.EmailCampaign.Core;
using Sitecore.Modules.EmailCampaign.Factories;
using Sitecore.Modules.EmailCampaign.Messages;
using Sitecore.Modules.EmailCampaign.Recipients;
using Sitecore.Modules.EmailCampaign.UI;
using Sitecore.Shell.Framework.Commands;
using ValtechBaseLine.Model.Common;
using ValtechBaseLine.RepositoryContract.Common;
using Sitecore.ListManagement.ContentSearch;
using Contact = Sitecore.Analytics.Tracking.Contact;
using ContactData = Sitecore.ListManagement.ContentSearch.Model.ContactData;
using ConFactory = Sitecore.Configuration.Factory;

namespace ValtechBaseLine.Repository.Common
{
    public class AnalyticsRepository:IAnalyticsContract
    {
        #region TriggerGoal
        public void TriggerGoal(string goalId,string message)
        {
            if (Sitecore.Analytics.Tracker.IsActive && Sitecore.Analytics.Tracker.Current.CurrentPage != null)
            {
                // Trigger a goal
                var goalItem = Sitecore.Context.Database.GetItem(goalId); // Goal item

                if (goalItem != null)
                {
                    var goal = new PageEventItem(goalItem); // Wrapper for goal
                    var pageEventsRow = Tracker.Current.CurrentPage.Register(goal); // Goal rec to be stored
                    pageEventsRow.Data = goalItem["Description"];
                    Tracker.Current.Interaction.AcceptModifications();
                }
            }
        }

        #endregion TriggerGoal

        #region NewsLetter-Subscription
        public ValidationResultModel AddingList(string emailAddress, string firstName, string lastName, string listId, out string contactId)
        {
            ValidationResultModel result = new ValidationResultModel();
            contactId = string.Empty;
            var listManager = Sitecore.Configuration.Factory.CreateObject("contactListManager", false) as ContactListManager;
            var contactRepo = ConFactory.CreateObject("contactListStore", true) as ContactListStore;
            ILockContext context = new LockContext(listId)
            {
                LockId = new ID(listId)
            };
            if (listManager != null)
            {
                var list = listManager.FindById(listId);
                List<ContactData> contacts = GetContactDatas(emailAddress, firstName, lastName, emailAddress);
                if (contactRepo != null) unlocklist(context, listId, contactRepo);
                if (contacts != null && contacts.Any())
                {
                  if (!isContactAlreadyAssociated(list,contacts))
                    {
                        listManager.AssociateContacts(list, contacts);
                        var firstOrDefault = contacts.FirstOrDefault();
                        if (firstOrDefault != null) contactId = firstOrDefault.ContactId.ToString();
                    }
                    else
                    {

                        result.ErrorMessage = "Contact is already availbale in Database.";
                    }
                }
                else
                {
                   
                    result.ErrorMessage = "Contact is already availbale in Database.";
                }
                if (contactRepo != null) unlocklist(context,listId,contactRepo);
            }
            return result;
        }

       

        public void SendSubscriptionMail(ID messageItemId, string contactId)
        {

            MessageItem message = Factory.GetMessage(messageItemId);
            Assert.IsNotNull(message, "Could not find message with ID " + messageItemId);
            RecipientId recipient = RecipientRepository.GetDefaultInstance().ResolveRecipientId("xdb:" + Guid.Parse(contactId));
            Assert.IsNotNull(recipient, "Could not find recipient with ID " + contactId);
            new AsyncSendingManager(message).SendStandardMessage(recipient);
        }

        #endregion NewsLetter-Subscription


        #region NewsLetter-Unsubscription
        public void RemovingContactsFromList(string listId, string contactId, string messageId)
        {
            System.Guid encryptedItem;
            if (messageId != null && System.Guid.TryParse(contactId, out encryptedItem))
            {
                using (GuidCryptoServiceProvider cryptoServiceProvider = GetCryptoServiceProvider(new Guid(messageId)))
                {
                    contactId = new ID(cryptoServiceProvider.Decrypt(encryptedItem)).ToString();
                }
            }
            var listManager = Sitecore.Configuration.Factory.CreateObject("contactListManager", false) as ContactListManager;
            var listStore = ConFactory.CreateObject("contactListStore", true) as ContactListStore;
            if (listManager != null)
            {
                 var contactRepos = ConFactory.CreateObject("contactRepository", true) as ContactRepository;
                Contact contact = null;
                if (contactRepos != null)
                {
                    if (contactId != null) contact = contactRepos.LoadContactReadOnly(new Guid(contactId));
                }
                var contacts = new List<ContactData>();
                if (contact != null)
                    contacts.Add(new ContactData
                    {
                        Identifier = contact.Identifiers.Identifier,
                        ContactId = contact.ContactId
                    });
                var list = listManager.FindById(listId);
                ILockContext context = new LockContext(listId)
                {
                    LockId = new ID(listId)
                };
                unlocklist(context, listId, listStore);
                if (isContactAlreadyAssociated(list, contacts))
                {
                    listManager.RemoveContactAssociations(list, contacts);
                }
                unlocklist(context, listId, listStore);
            }

        }

        #endregion NewsLetter-Unsubscription


        #region Private Methods
        protected ContactData CreateContact(string identifier, string firstName, string lastName, string emailAddress)
        {
            if (String.IsNullOrEmpty(identifier))
            {
                return null;
            }
            var contactRepo = ConFactory.CreateObject("contactRepository", true) as ContactRepository;
            LeaseOwner leaseOwner = new LeaseOwner(identifier, LeaseOwnerType.OutOfRequestWorker);

            Contact newContact = null;

            LockAttemptResult<Contact> result = contactRepo.TryLoadContact(identifier, leaseOwner, TimeSpan.FromMinutes(1.0));
            switch (result.Status)
            {
                case LockAttemptStatus.Success:
                    // Existing Contact
                    newContact = result.Object as Contact;

                    // Email Facet
                    var emailFacet = newContact.GetFacet<IContactEmailAddresses>("Emails"); //_facetEmails = "Emails";
                    if (!string.IsNullOrEmpty(emailFacet.Preferred))
                    {
                        IEmailAddress address = emailFacet.Entries[emailFacet.Preferred];
                        address.SmtpAddress = emailAddress;
                    }
                    emailFacet.Preferred = "Preferred"; //_facetEmailPreferred = "Preferred";
                    break;
                case LockAttemptStatus.NotFound:
                    // New Contact
                    ID guid = Sitecore.Data.ID.NewID;
                    newContact = contactRepo.CreateContact(guid);
                    newContact.Identifiers.Identifier = identifier;

                    // Email Facet
                    var emailFacetNew = newContact.GetFacet<IContactEmailAddresses>("Emails"); //_facetEmails = "Emails";
                    emailFacetNew.Entries.Create("Preferred").SmtpAddress = emailAddress;
                    emailFacetNew.Preferred = "Preferred";

                    break;
                default:
                    newContact= contactRepo.LoadContactReadOnly(identifier);
                    break;
                   // return oldcontact;
            }

            if (newContact != null)
            {
                // Create and Update same once we have the contact
                var personalInfoNew = newContact.GetFacet<IContactEmailAddresses>("Emails");
                var info = newContact.GetFacet<IContactPersonalInfo>("Personal");
                info.FirstName = firstName;
                info.Surname = lastName;
                //personalInfoNew.FirstName = firstName;
                //personalInfoNew.Surname = lastName;
            }
            newContact.ContactSaveMode = ContactSaveMode.AlwaysSave;
            try
            {
                contactRepo.SaveContact(newContact, new ContactSaveOptions(true, leaseOwner));
            }
            catch (Exception ExContactNotSaved)
            {
                //if (ExContactNotSaved Error is exists and not testing for existence)
                string message = ExContactNotSaved.Message.ToString();
            }
            return new ContactData()
            {
                Identifier = identifier,
                ContactId = newContact.ContactId
            };
        }


        protected List<ContactData> GetContactDatas(string identifier, string firstName, string lastName, string emailAddress)
        {
            List<ContactData> returnList = new List<ContactData>();
            ContactData contact = CreateContact(identifier, firstName, lastName, emailAddress);
            if (contact != null) returnList.Add(contact);

            return returnList;
        }

        private void ClearSharedSessionLocks(ContactManager manager, ContactData contact)
        {
            if (HttpContext.Current != null && HttpContext.Current.Session != null)
                return;

            var sharedSessionStateManagerField = manager.GetType().GetField("sharedSessionStateManager", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            Assert.IsNotNull(sharedSessionStateManagerField, "Didn't find field 'sharedSessionStateManager' in type '{0}'.", typeof(ContactManager));
            var sssm = (SharedSessionStateManager)sharedSessionStateManagerField.GetValue(manager);
            Assert.IsNotNull(sssm, "Shared session state manager field value is null.");

            var contactLockIdsProperty = sssm.GetType().GetProperty("ContactLockIds", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            Assert.IsNotNull(contactLockIdsProperty, "Didn't find property 'ContactLockIds' in type '{0}'.", sssm.GetType());
            var contactLockIds = (Dictionary<Guid, object>)contactLockIdsProperty.GetValue(sssm);
            Assert.IsNotNull(contactLockIds, "Contact lock IDs property value is null.");
            contactLockIds.Remove(contact.ContactId);
        }


        private bool isContactAlreadyAssociated(ContactList list, List<ContactData> contacts)
        {
            var listManager = Sitecore.Configuration.Factory.CreateObject("contactListManager", false) as ContactListManager;
            var ExistingContact = listManager.GetContacts(list);
            return Enumerable.Any(ExistingContact, existContact =>
            {
                var firstOrDefault = contacts.FirstOrDefault();
                return firstOrDefault != null && existContact.ContactId.Equals(firstOrDefault.ContactId);
            });
        }

        private void unlocklist(ILockContext context, string listId, ContactListStore listStore)
        {
            if (listStore != null && listStore.GetLockedListIds().Contains(listId))
            {
                listStore.Unlock(context);
            }

        }
        #endregion Private Methods


        internal virtual GuidCryptoServiceProvider GetCryptoServiceProvider(System.Guid messageId)
        {

             return new ValtechBaseLine.CMS.Extension.GuidCryptoServiceProviderDefault(messageId);
        }
    }


}
