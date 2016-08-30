using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Mvc;
using Glass.Mapper.Sc;
using Microsoft.Owin.Security;
using Sitecore.Mvc.Presentation;
using ValtechBaseLine.Common;
using ValtechBaseLine.Model.Components;
using ValtechBaseLine.RepositoryContract.Components;
using ValtechBaseLine.Validation;
using ValtechBaseLine.Web.CustomFilters;
using ValtechBaseLine.Web.Helpers;

namespace ValtechBaseLine.Web.Controllers
{
    public class RegistrationController : Controller
    {
        private readonly SitecoreContext _sitecoreContext;
        private readonly IRegistrationContract _registrationContract;

        public RegistrationController(IRegistrationContract registrationContract)
        {
            _sitecoreContext = new SitecoreContext();
            _registrationContract = registrationContract;
        }

        public ActionResult GetRegistrationForm()
        {
            RegistrationModel registration = default(RegistrationModel);
            string dataSource = RenderingContext.Current.Rendering.DataSource;
            if (!string.IsNullOrWhiteSpace(dataSource))
            {
                registration = _registrationContract.GetRegistrationForm(dataSource);
            }
            return View("~/Views/Component/RegistrationView.cshtml", registration);

        }

       

        [ReCaptcha]
        [ValidateAntiForgeryToken()]
        public ActionResult SubmitRegistrationForm(RegistrationModel model, bool captchaAvailableOnPage, bool captchaValid)
        {
            string duplicateEmail;
            var captchaValidation = (!captchaAvailableOnPage) || captchaValid;
            if (captchaValidation)
            {
                var errorMessage = ModelState.Values.Where(x => x.Errors.Count > 0).ToList();
                if (ModelState.IsValid && !errorMessage.Any())
                {
                    var saveData = _registrationContract.SubmitRegistrationForm(model, out duplicateEmail);
                    if (duplicateEmail == Constants.DuplicateEmail)
                    {
                        return Json(new { Message = string.Join("<br/>", Sitecore.Globalization.Translate.Text(ValidationConstants.ValidationKeys.DuplicateEmailAddress)), Status = "Fail" });
                    }
                    if (saveData)
                    {
                        Sitecore.Data.Items.Item homeItem = Sitecore.Context.Database.GetItem(Constants.ValtechHomePage);
                        string homeUrl = string.Empty;
                        if (homeItem != null)
                        {
                            homeUrl = Sitecore.Links.LinkManager.GetItemUrl(homeItem);
                        }
                        Session["oauthEmail"] =  model.EmailId;
                        return Json(new { Message = string.Join("<br/>", Sitecore.Globalization.Translate.Text(ValidationConstants.ValidationKeys.FormSubmittedSuccessfully)), Status = "Success", url = homeUrl });
                    }

                    return Json(new { Message = string.Join("<br/>", Sitecore.Globalization.Translate.Text(ValidationConstants.ValidationKeys.TryAgain)), Status = "Fail" });

                }
                var errormessages = errorMessage.SelectMany(x => x.Errors.ToList()).Select(x => x.ErrorMessage).ToList();

                return Json(new { Message = string.Join("<br/>", errormessages), Status = "Fail" });

            }

            return Json(new { Message = string.Join("<br/>", Sitecore.Globalization.Translate.Text(ValidationConstants.ValidationKeys.InvalidReCaptcha)), Status = "Fail" });
        }
    }
}