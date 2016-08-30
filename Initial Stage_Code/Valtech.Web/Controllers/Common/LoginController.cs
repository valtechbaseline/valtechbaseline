using System;
using System.Web;
using System.Web.Mvc;
//using System.Web.Security;
//using Microsoft.AspNet.Identity;
using System.Web.Security;
using Glass.Mapper.Sc;
using Microsoft.Owin.Security;
using Sitecore.Links;
using Sitecore.Security;
using ValtechBaseLine.Model.Components;
using ValtechBaseLine.Common;


namespace ValtechBaseLine.Web.Controllers.Common
{
    public class LoginController : Controller
    {
        /// <summary>
        /// Get Owin AuthenticationManager
        /// </summary>
        private Microsoft.Owin.Security.IAuthenticationManager AuthenticationManager
        {
            get { return HttpContext.GetOwinContext().Authentication; }
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult FacebookLogin(string returnUrl,string provider)
        {
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Login", 
                new { loginProvider = provider, ReturnUrl = returnUrl }));
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult GoogleLogin(string returnUrl, string provider)
        {
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Login",
                new { loginProvider = provider, ReturnUrl = returnUrl }));
        }
        

        [AllowAnonymous]
        public ActionResult ExternalLoginCallback(string returnUrl)
        {
            try
            {
                if (string.IsNullOrEmpty(returnUrl))
                    returnUrl = "~/";
                var loginInfo = AuthenticationManager.GetExternalLoginInfo();
                if (loginInfo == null)
                {
                    throw new Exception("External provider authenticated data absent");
                }
                var externAuthModel = new ExternalAuthLoginModel
                {
                    Email = loginInfo.Email,
                    LoginProviderId = loginInfo.Login.LoginProvider,
                    ProviderAuthKey = loginInfo.Login.ProviderKey,
                    Name = loginInfo.ExternalIdentity.Name
                };

                Session["AuthUserName"] = externAuthModel.Name;

                //DoLogin(serviceResponse.ApplicationUser);
                return Redirect(returnUrl);
            }
            catch (Exception ex)
            {
                return Redirect(returnUrl);
            }
        }

        public ActionResult Logout()
        {
            Sitecore.Security.Authentication.AuthenticationManager.Logout();
            Session.Abandon();
            Session.Clear();

            Sitecore.Links.UrlOptions urlOptions = UrlOptions.DefaultOptions;
            urlOptions.LanguageEmbedding = LanguageEmbedding.Always;
            var startItem = Sitecore.Context.Database.GetItem(Sitecore.Context.Site.ContentStartPath);
            var url = Sitecore.Links.LinkManager.GetItemUrl(startItem, urlOptions);

            return Redirect(url);
        }

        public ActionResult DoLogin(string emailId, string password, string rememberMeChk)
        {
            if (string.IsNullOrWhiteSpace(emailId)) return null;
            string qualifiedName = string.Concat(Constants.ExtranetUsersDomainName, "\\", emailId);
            var user = Membership.GetUser(qualifiedName);
            if (user == null) return null;
           // var role = UserRoles.FromUser(Sitecore.Security.Accounts.User.FromName(qualifiedName, true));
            UserProfile userProfile = Sitecore.Security.Accounts.User.FromName(qualifiedName, true).Profile;

            var isauthenticate = Sitecore.Security.Authentication.AuthenticationManager.Login(
                string.Concat(Constants.ExtranetUsersDomainName, "\\", emailId), password, false);
            Session["AuthUserName"] = userProfile.FullName;
            if (Convert.ToBoolean(rememberMeChk))
            {
                var cookie = new HttpCookie("ValtechBaseline");
                cookie.Values.Add("username", Util.Encrypt(emailId));
                cookie.Expires = DateTime.Now.AddDays(1);
                Response.Cookies.Add(cookie);
            }
            else
            {
                var cookies = HttpContext.Request.Cookies;
                if (cookies != null && cookies["ValtechBaseline"] != null)
                {
                    var cookie = cookies["ValtechBaseline"];
                    cookie.Expires = DateTime.Now.AddDays(-1);
                    HttpContext.Response.Cookies.Add(cookie);
                }
            }
            if (isauthenticate)
            {
                return Json(new {Message = "User Authenticated Successfully", Status = "Success",});
            }
            else
            {
                return Json(new { Message = "User not Authenticated ", Status = "Fail", });
            }
            
        }
    }

    public class ChallengeResult : HttpUnauthorizedResult
    {
        public ChallengeResult(string provider, string redirectUri)
            : this(provider, redirectUri, null)
        {
        }

        public ChallengeResult(string provider, string redirectUri, string userId)
        {
            LoginProvider = provider;
            RedirectUri = redirectUri;
            UserId = userId;
        }

        public string LoginProvider { get; set; }
        public string RedirectUri { get; set; }
        public string UserId { get; set; }
        private const string XsrfKey = "CodePaste_$31!.2*#";

        public override void ExecuteResult(ControllerContext context)
        {
            var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
            if (UserId != null)
                properties.Dictionary[XsrfKey] = UserId;

            var owin = context.HttpContext.GetOwinContext();
            owin.Authentication.Challenge(properties, LoginProvider);
        }
    }
}