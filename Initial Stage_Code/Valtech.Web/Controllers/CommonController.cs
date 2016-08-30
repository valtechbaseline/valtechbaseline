﻿using System;
﻿using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
﻿using System.Collections.Generic;
﻿using System.Web;
﻿using System.Web.Mvc;
using Glass.Mapper.Sc;
﻿using Microsoft.Owin.Security;
﻿using Sitecore.Analytics;
﻿using Sitecore.Analytics.Data.Items;
using Sitecore.Analytics.Model.Entities;
using Sitecore.Data;
using Sitecore.Mvc.Presentation;
﻿using Sitecore.Security;
﻿using Sitecore.SecurityModel;
using Sitecore.Social.Api;
﻿using Sitecore.Social.Domain.Model;
﻿using Sitecore.Social.Facebook.Networks;
using Sitecore.Social.Infrastructure.Extensions;
using ValtechBaseLine.Model.Common;
using ValtechBaseLine.Common;
using ValtechBaseLine.Model.Components;
using ValtechBaseLine.RepositoryContract.Common;
﻿using Constants = ValtechBaseLine.Common.Constants;


namespace ValtechBaseLine.Web.Controllers
{
    public class CommonController : Controller
    {
        private readonly SitecoreContext _sitecoreContext;
        private readonly IHeaderContract _myClass;
        private readonly IAnalyticsContract _analyticsContract;
        private readonly ITrackVisitor _trackContract;
        public string RecepientListGuid;
        private string UnsubRecepientListGuid;
        private readonly IFooterContract _footer;

        public CommonController(IHeaderContract myClass, IFooterContract footer, IAnalyticsContract commonContract, ITrackVisitor trackContract)//,IEmailSender email)
        {
            _sitecoreContext = new SitecoreContext();
            _myClass = myClass;
            _analyticsContract = commonContract;
            _footer = footer;
            _trackContract = trackContract;
        }

        public ActionResult Footer()
        {
            IFooterModel footerDetails = default(IFooterModel);
            if (!string.IsNullOrWhiteSpace(RenderingContext.Current.Rendering.DataSource))
            {
                footerDetails = _footer.GetFooterDetails(RenderingContext.Current.Rendering.DataSource);
            }
            return View(footerDetails);
        }

        public ActionResult TrackingUser()
        {
            TrackUserModelCollection trackCollection = _trackContract.GetBrowserCookieValue();
            return View(trackCollection);
        }

        /// <summary>
        /// Get Owin AuthenticationManager
        /// </summary>
        private Microsoft.Owin.Security.IAuthenticationManager AuthenticationManager
        {
            get { return HttpContext.GetOwinContext().Authentication; }
        }
        


        public ActionResult Header()
        {
            if (Session["oauthEmail"] != null)
            {
                    string qualifiedName = string.Concat(Constants.ExtranetUsersDomainName, "\\", Session["oauthEmail"]);
                    UserProfile userProfile = Sitecore.Security.Accounts.User.FromName(qualifiedName, true).Profile;
                    Session["AuthUserName"] = userProfile.FullName;

            }
            IHeaderModel baseDetails = default(IHeaderModel);
            if (!string.IsNullOrWhiteSpace(RenderingContext.Current.Rendering.DataSource))
            {
                baseDetails = _myClass.GetHeaderDetails(RenderingContext.Current.Rendering.DataSource);
            }
            SetSocialLoginValues();
            return View(baseDetails);

        }

        public ActionResult NewsLetterSignUp()
        {
            NewsLetterSignupModel newsLetterModel = new NewsLetterSignupModel();
            if (!string.IsNullOrWhiteSpace(RenderingContext.Current.Rendering.DataSource))
            {
                // baseDetails = _myClass.GetHeaderDetails(RenderingContext.Current.Rendering.DataSource);
            }
            return View(newsLetterModel);

        }

        private Dictionary<string, string> GetProfileValues()
        {
            var profileProps = new Dictionary<string, string>();
            foreach (var customPropertyName in Sitecore.Context.User.Profile.GetCustomPropertyNames())
            {
                if (customPropertyName.Contains("gg_"))
                {
                    profileProps.Add(customPropertyName, Sitecore.Context.User.Profile.GetCustomProperty(customPropertyName));

                }
                if (customPropertyName.Contains("fb_"))
                {
                    profileProps.Add(customPropertyName, Sitecore.Context.User.Profile.GetCustomProperty(customPropertyName));

                }
            }
            return profileProps;
        }

        private void CheckLogin()
        {
            if (Sitecore.Context.User.IsAuthenticated)
            {

            }
        }

        private void SetSocialLoginValues()
        {
            string googleNetworkId = "";
            string facebookNetworkId = "";
            if (Sitecore.Context.User.IsAuthenticated)
            {
                var userProfileProps = GetProfileValues();
                if (userProfileProps != null && userProfileProps.Count > 0)
                {

                    var socialNetWork = Sitecore.Context.User.Profile.GetCustomProperty("SC_SocialNetworkId");
                    if (socialNetWork != null)
                    {
                        var networkIds = socialNetWork.Split(':');
                        foreach (var networkId in networkIds)
                        {
                            if (networkId.Contains("gg_"))
                            {
                                googleNetworkId = networkId;
                            }
                            if (networkId.Contains("fb_"))
                            {
                                facebookNetworkId = networkId;
                                string fbData = "";
                                foreach (
                                    var customPropertyName in Sitecore.Context.User.Profile.GetCustomPropertyNames())
                                {
                                    if (customPropertyName.Contains("fb_"))
                                    {
                                        fbData += customPropertyName + ": " +
                                                  Sitecore.Context.User.Profile.GetCustomProperty(customPropertyName) +
                                                  "";
                                    }
                                }
                            }
                        }
                    }


                    var externAuthModel = new ExternalAuthLoginModel
                    {
                        //Email = userProfileProps["gg_basicData_email"],

                        Name = Sitecore.Context.User.Profile.FullName
                    };

                    Session["AuthUserName"] = externAuthModel.Name;
                    var socialProfileManager = new SocialProfileManager();
                    SocialProfile googleprofile = new SocialProfile();
                    if (Tracker.Current != null && Tracker.Current.Contact != null)
                    {
                        googleprofile =
                            socialProfileManager.GetSocialProfile(Tracker.Current.Contact.ContactId.GetIdentifier(),
                                "Google");


                        if (socialProfileManager.SocialProfileExists(Sitecore.Context.User.Name, "Facebook"))
                        {
                            var fbprofile =
                                socialProfileManager.GetSocialProfile(Tracker.Current.Contact.ContactId.GetIdentifier(),
                                    "Facebook");
                        }
                    }

                    if (googleprofile.Fields != null && googleprofile.Fields.Count > 0)
                        Session["LoggedInImgurl"] = googleprofile.Fields["gg_ImageUrl"];
                    //if (fbprofile.Fields.Count > 0)
                    //    Session["LoggedInImgurl"] = fbprofile.Fields["fb_ImageUrl"];



                    if (Tracker.Current != null)
                    {
                        var profile =
                            socialProfileManager.GetSocialProfile(Tracker.Current.Contact.ContactId.GetIdentifier(),
                                "Google");
                    }

                    //Session["LoggedInImgurl"] = profile.Fields["gg_ImageUrl"];

                }


            }

            //if (!String.IsNullOrEmpty(userProfile))
            //{
            //    var email = userProfile; // update UI here
            //}
            //if (Sitecore.Context.User.IsAuthenticated)
            //{
            //    var socialProfile = new SocialProfileManager();
            //    // Do we have an extra profile from the user?
            //    if (socialProfile.SocialProfileExists(userProfile, "Google"))
            //    {
            //        var facebookProfile = socialProfile.GetSocialProfile(userProfile, "Google");
            //        if (facebookProfile.Fields["gg_FamilyName"] != null)
            //        {
            //            // do something here
            //        }
            //    }
            //}
        }

        public JsonResult NewsletterSignupConfirmed(NewsLetterSignupModel newslettermodel)
        {
            string contactId;
            if (newslettermodel != null)
            {
                using (new SecurityDisabler())
                {
                    var result = _analyticsContract.AddingList(newslettermodel.EmailId, newslettermodel.FirstName, newslettermodel.LastName, ValtechBaseLine.Common.Constants.list.NewsLetterSubscriptionList, out contactId);
                    if (!string.IsNullOrEmpty(result.ErrorMessage))
                    {
                        return Json(new { Status = "ValidationError", Message = result.ErrorMessage }, JsonRequestBehavior.AllowGet);
                    }
                    _analyticsContract.SendSubscriptionMail(new ID(ValtechBaseLine.Common.Constants.NewsLetterSubscriptionMail), contactId);
                }
            }
            return Json(new { Status = "Success", Message = SitecoreHelper.GetDictionaryValueByKey(ValtechBaseLine.Common.Constants.SuccessNewsLetter) }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DownloadAnalytics()
        {
            _analyticsContract.TriggerGoal("{F22AE91A-6AEC-4B6D-BB07-2D0082D43A19}", "user has clicked on Article Page");
            var jsonData = new
            {
                Success = true,
                Message = "Successfully registered goal",

            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

    }
}