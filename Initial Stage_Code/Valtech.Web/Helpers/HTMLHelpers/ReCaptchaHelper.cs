using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Castle.Core.Internal;
using Glass.Mapper.Sc;
using Newtonsoft.Json;
using Sitecore.ContentSearch.Abstractions;
using ValtechBaseLine.Model.Common;
using Constants = ValtechBaseLine.Common.Constants;

namespace ValtechBaseLine.Web.Helpers
{
    public static class ReCaptchaHelper
    {
        private static SitecoreContext _sitecoreContext;
        static ReCaptchaHelper()
        {
            _sitecoreContext = new SitecoreContext();

        }
        public static IHtmlString ReCaptcha(this HtmlHelper helper)
        {
            StringBuilder sb = new StringBuilder();
            string publickey = _sitecoreContext.GetItem<ISiteSettingModel>(Constants.SiteSettings).PublicKey;
            sb.AppendLine("<script type=\"text/javascript\" src='https://www.google.com/recaptcha/api.js'></script>");
            sb.AppendLine("");
            sb.AppendLine("<div class=\"g-recaptcha\" data-sitekey=\"" + publickey + "\"></div>");
            return MvcHtmlString.Create(sb.ToString());
        }
    }
}
