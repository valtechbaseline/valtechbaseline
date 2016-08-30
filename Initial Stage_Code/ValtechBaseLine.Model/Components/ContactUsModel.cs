using Glass.Mapper.Sc.Configuration.Attributes;
using System.Collections.Generic;
using System.IO;
using Sitecore.Data.Items;
using System.Collections.Specialized;
using ValtechBaseLine.Validation;
using ValtechBaseLine.Validation.Validations;

namespace ValtechBaseLine.Model.Components
{
    [SitecoreType(AutoMap = true)]
    public class ContactUsModel
    {
        [SitecoreField("EmailMessage")]
        public virtual string EmailMessage { get; set; }

        [SitecoreField("ToEmail")]
        public virtual string ToEmail { get; set; }

        [SitecoreField("FromEmail")]
        public virtual string FromEmail { get; set; }

        [SitecoreField("Subject")]
        public virtual string Subject { get; set; }


        [SitecoreField("IsHtmlMessage")]
        public virtual bool IsHtmlMessage { get; set; }

        [SitecoreField("Token")]
        public virtual IEnumerable<EmailTokenModel> Token { get; set; }




        [SitecoreQuery("/sitecore/content//*[@@templatename='Alert Messages']")]
        public Item AlertMessages { get; set; }

        

        // form labels
        [ValtechRequiredAttribute(ValidationConstants.ValidationKeys.FirstNameRequired)]
        public string FirstName { get; set; }

        [ValtechRequiredAttribute(ValidationConstants.ValidationKeys.LastNameRequired)]
        public string LastName { get; set; }

        [ValtechRequired(ValidationConstants.ValidationKeys.EmailIdRequired)]
        [ValtechRegularExpression(ValidationConstants.ValidationKeys.InvalidEmail, @"[A-Za-z-0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}")]
        public string UserEmail { get; set; }

        public string MailSubject { get; set; }
        public string Message { get; set; }
        public Stream FileStream { get; set; }
        public string FileName { get; set; }
        public string DeliveryMessage { get; set; }

        public string IncludeJsScript { get; set; }
        public string IncludePageScript { get; set; }
        public NameValueCollection SiteMessages { get; set; }

        public string GoogleMarker { get; set; }

        public bool captchaValid { get; set; }

        public bool IsSent { get; set; }

        public string FailureMessage { get; set; }

    }
}
