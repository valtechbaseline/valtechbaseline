using System.Configuration;
using System.Security.Claims;
using System.Web.Helpers;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;

namespace ValtechBaseLine.Web
{
    public partial class Startup
    {
        public void ConfigureAuth(IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/ValtechBaseline/Account/start"),
                CookieSecure = CookieSecureOption.SameAsRequest
            });
            
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);
            var facebookAppId = ConfigurationManager.AppSettings[ValtechBaseLine.Common.Constants.FacebookAppId];
            var facebookSecretKey = ConfigurationManager.AppSettings[ValtechBaseLine.Common.Constants.FacebookAppSecretKey];

            app.UseFacebookAuthentication(facebookAppId ,facebookSecretKey);

            var googleClientId = ConfigurationManager.AppSettings[ValtechBaseLine.Common.Constants.GoogleAppClientId];
            var googleSecretKey = ConfigurationManager.AppSettings[ValtechBaseLine.Common.Constants.GoogleAppSecretKey];
            app.UseGoogleAuthentication(googleClientId,googleSecretKey);

            AntiForgeryConfig.UniqueClaimTypeIdentifier = ClaimTypes.NameIdentifier;
        }
    }
}