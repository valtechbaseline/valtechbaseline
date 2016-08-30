using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Glass.Mapper.Sc;
using Newtonsoft.Json;
using ValtechBaseLine.Common;
using ValtechBaseLine.Model.Common;

namespace ValtechBaseLine.Web.CustomFilters
{
    public class ReCaptchaAttribute : ActionFilterAttribute
    {
        private static SitecoreContext _sitecoreContext;
        public ReCaptchaAttribute()
        {
            _sitecoreContext = new SitecoreContext();
        }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.RequestContext.HttpContext.Request["g-recaptcha-response"] != null)
            {
                filterContext.ActionParameters["CaptchaAvailableOnPage"] = true;
                string privatekey = _sitecoreContext.GetItem<ISiteSettingModel>(Constants.SiteSettings).PrivateKey;
                string response = filterContext.RequestContext.HttpContext.Request["g-recaptcha-response"];
                filterContext.ActionParameters["captchaValid"] = Validate(response, privatekey);
            }
            else
            {
                filterContext.ActionParameters["captchaAvailableOnPage"] = false;
                filterContext.ActionParameters["captchaValid"] = false;
            }
           
        }

        public static bool Validate(string mainResponse, string privateKey)
        {
            try
            {
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create
                ("https://www.google.com/recaptcha/api/siteverify?secret=" + privateKey + "&response=" + mainResponse);

                WebResponse response = req.GetResponse();

                using (StreamReader readStream = new StreamReader(response.GetResponseStream()))
                {
                    string jsonResponse = readStream.ReadToEnd();

                    JsonResponseObject jobj = JsonConvert.DeserializeObject<JsonResponseObject>(jsonResponse);

                    return jobj.Success;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public class JsonResponseObject
        {
            public bool Success { get; set; }
            [JsonProperty("error-codes")]
            public List<string> Errorcodes { get; set; }
        }
    }
}