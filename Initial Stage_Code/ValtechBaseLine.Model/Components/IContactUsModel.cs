using Glass.Mapper.Sc.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValtechBaseLine.Model.Components
{
    public interface IContactUsModel
    {
        [SitecoreField("EmailMessage")]
        string EmailMessage { get; set; }

        [SitecoreField("ToEmail")]
        string ToEmail { get; set; }

        [SitecoreField("FromEmail")]
        string FromEmail { get; set; }

        [SitecoreField("Subject")]
        string Subject { get; set; }

        [SitecoreField("SMTPServer")]
        string SMTPServer { get; set; }

        [SitecoreField("IsHtmlMessage")]
        bool IsHtmlMessage { get; set; }

         [SitecoreField("Token")]
        IEnumerable<IEmailTokenModel> Token { get; set; }


        // form labels
         string LastName { get; set; }
         string FirstName { get; set; }

        

    }
}
