using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Analytics;
using Sitecore.Analytics.Data;
using Sitecore.Analytics.DataAccess;
using Sitecore.Analytics.Model;
using Sitecore.Analytics.Tracking;
using Sitecore.Analytics.Tracking.SharedSessionState;
using Sitecore.Configuration;
using Sitecore.Data;
using Sitecore.Diagnostics;


namespace ValtechBaseLine.CMS.Extension.MongoDB
{
    public class ExtendedContactRepository
    {
        public Contact GetOrCreateContact(string userName)
        {
            if (IsContactInSession(userName))
                return Tracker.Current.Session.Contact;

            ContactRepository contactRepository = Factory.CreateObject("tracking/contactRepository", true) as ContactRepository;
            ContactManager contactManager = Factory.CreateObject("tracking/contactManager", true) as ContactManager;

            Assert.IsNotNull(contactRepository, "contactRepository");
            Assert.IsNotNull(contactManager, "contactManager");

            try
            {
                Contact contact = contactRepository.LoadContactReadOnly(userName);
                LockAttemptResult<Contact> lockAttempt;

                if (contact == null)
                    lockAttempt = new LockAttemptResult<Contact>(LockAttemptStatus.NotFound, null, null);
                else
                    lockAttempt = contactManager.TryLoadContact(contact.ContactId);

                return GetOrCreateContact(userName, lockAttempt, contactRepository, contactManager);
            }
            catch (Exception ex)
            {
                throw new Exception(this.GetType() + " Contact could not be loaded/created - " + userName, ex);
            }
        }

        public void ReleaseAndSaveContact(Contact contact)
        {
            ContactManager manager = Factory.CreateObject("tracking/contactManager", true) as ContactManager;
            if (manager == null)
                throw new Exception(this.GetType() + " Could not instantiate " + typeof(ContactManager));
            manager.SaveAndReleaseContact(contact);
            ClearSharedSessionLocks(manager, contact);
        }

        private Contact GetOrCreateContact(string userName, LockAttemptResult<Contact> lockAttempt, ContactRepository contactRepository, ContactManager contactManager)
        {
            switch (lockAttempt.Status)
            {
                case LockAttemptStatus.Success:
                    Contact lockedContact = lockAttempt.Object;
                    lockedContact.ContactSaveMode = ContactSaveMode.AlwaysSave;
                    return lockedContact;

                case LockAttemptStatus.NotFound:
                    Contact createdContact = CreateContact(userName, contactRepository);
                    contactManager.FlushContactToXdb(createdContact);
                    return GetOrCreateContact(userName);

                default:
                    throw new Exception(this.GetType() + " Contact could not be locked - " + userName);
            }
        }

        private Contact CreateContact(string userName, ContactRepository contactRepository)
        {
            Contact contact = contactRepository.CreateContact(ID.NewID);
            contact.Identifiers.Identifier = userName;
            contact.System.Value = 0;
            contact.System.VisitCount = 0;
            contact.ContactSaveMode = ContactSaveMode.AlwaysSave;
            return contact;
        }

        private bool IsContactInSession(string userName)
        {
            var tracker = Tracker.Current;

            if (tracker != null &&
                tracker.IsActive &&
                tracker.Session != null &&
                tracker.Session.Contact != null &&
                tracker.Session.Contact.Identifiers != null &&
                tracker.Session.Contact.Identifiers.Identifier != null &&
                tracker.Session.Contact.Identifiers.Identifier.Equals(userName, StringComparison.InvariantCultureIgnoreCase))
                return true;

            return false;
        }

        private void ClearSharedSessionLocks(ContactManager manager, Contact contact)
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
    }
}
