using Glass.Mapper.Sc;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ValtechBaseLine.Common;
using ValtechBaseLine.Model.Common;
using ValtechBaseLine.Model.Components;
using ValtechBaseLine.RepositoryContract.Components;

namespace ValtechBaseLine.Repository.Components
{
    public class ContactUsRepository : IContactUsContract
    {
        private readonly SitecoreContext _sitecoreContext;
        private readonly IEmailSender _emailContext;

        /// <summary>
        /// Default Constructor
        /// Instantiates the Sitecore service
        /// </summary>
        public ContactUsRepository(IEmailSender emailContext)
        {
            _sitecoreContext = new SitecoreContext();
            _emailContext = emailContext;
        }

        public ContactUsModel GetContactUsDetails(string datasource)
        {
            var model = this._sitecoreContext.GetItem<ContactUsModel>(new Guid(datasource));
            if (model.AlertMessages != null)
            {
                string _urlParamsToParse = model.AlertMessages["SiteMessages"];
                model.SiteMessages = Sitecore.Web.WebUtil.ParseUrlParameters(_urlParamsToParse);
               
            }
            return model;
        }



        public object GetPropValue(object src, string propName)
        {
            propName = propName.Replace("{", "").Replace("}", "");
            return src.GetType().GetProperty(propName).GetValue(src, null);
        }

        public EmailDTO MappingContactUs(ContactUsModel contactUsModel)
        {

            StringBuilder builder = new StringBuilder();
            builder.Append(contactUsModel.EmailMessage);
            foreach (var s in contactUsModel.Token)
            {
                object val = GetPropValue(contactUsModel, s.Key);
                if (val!=null)
                 builder.Replace(s.Key, val.ToString());
            }
            var regex = new Regex("[{][^{]*[}]");

            var output = regex.Replace(builder.ToString(), String.Empty);
            var EmailDTO = new EmailDTO()
            {
                From = contactUsModel.FromEmail,
                To = contactUsModel.ToEmail.Split(';'),
                Subject = contactUsModel.Subject,
                IsHtmlMessage = contactUsModel.IsHtmlMessage,
                Message = output.ToString(),
                FileName = contactUsModel.FileName != null ? contactUsModel.FileName : "",
                FileStream = contactUsModel.FileStream != null ? contactUsModel.FileStream : null
            };


            return EmailDTO;
        }
    }
}
