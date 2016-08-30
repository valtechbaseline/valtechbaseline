using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using ValtechBaseLine.Model.Interfaces;

namespace ValtechBaseLine.Model.Components
{
    public class Recaptcha : IRecaptcha
    {
        public bool ValidateCaptcha(string responseLink,string privateKey)
        {
            string secret = privateKey;
            var client = new WebClient();
            var reply = client.DownloadString(string.Format(responseLink, secret, HttpContext.Current.Request["g-recaptcha-response"]));
            var captchaResponse = JsonConvert.DeserializeObject<CaptchaResponse>(reply);
            return Convert.ToBoolean(captchaResponse.Success);
        }
    }
}