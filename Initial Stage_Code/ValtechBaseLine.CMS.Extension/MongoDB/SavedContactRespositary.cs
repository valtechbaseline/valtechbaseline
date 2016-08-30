using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore.Analytics;
using Sitecore.Analytics.Model.Entities;
using Sitecore.Analytics.Tracking;
using ValtechBaseLine.Model.Components;

namespace ValtechBaseLine.CMS.Extension.MongoDB
{
    public class SavedContactRespositary
    {
        public void XdbSaveContact(RegistrationModel smodel)
        {
            Tracker.Current.Session.Identify(smodel.EmailId);

            // Create an instance of the repository
            ExtendedContactRepository extendedContactRepository = new ExtendedContactRepository();
            // Get a contact by a username
            Contact contact = extendedContactRepository.GetOrCreateContact(smodel.EmailId);

            // Do some code that updates the contact

            //var contact = Tracker.Current.Session.Contact;
            if (Sitecore.Analytics.Tracker.Current.Contact != null)
            {
                var emailFacet = contact.GetFacet<IContactEmailAddresses>("Emails");
                //Check if an work email address already exists for the contact
                if (!emailFacet.Entries.Contains("Work Email"))
                {
                    IEmailAddress email = emailFacet.Entries.Create("Work Email");
                    email.SmtpAddress = smodel.EmailId;
                    emailFacet.Preferred = "Work Email";
                }
            }
            if (Sitecore.Analytics.Tracker.Current.Contact != null)
            {
                var personalFacet = contact.GetFacet<IContactPersonalInfo>("Personal");
                personalFacet.FirstName = smodel.FirstName;
                personalFacet.Surname = smodel.LastName;
                personalFacet.Suffix = smodel.SelectedSalutation;
                

            }
            if (Sitecore.Analytics.Tracker.Current.Contact != null)
            {
                var phoneFacet = contact.GetFacet<IContactPhoneNumbers>("Phone Numbers");
                if (!phoneFacet.Entries.Contains("Cell Phone"))
                {
                    IPhoneNumber cellPhone = phoneFacet.Entries.Create("Cell Phone");
                    cellPhone.Number = smodel.PhoneNumber;
                  
                }
            }

            // Save the contact
            extendedContactRepository.ReleaseAndSaveContact(contact);
        }
    }
}
