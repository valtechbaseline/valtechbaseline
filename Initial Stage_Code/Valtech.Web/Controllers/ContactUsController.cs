using Glass.Mapper.Sc;
using Sitecore.Analytics;
using Sitecore.Mvc.Presentation;
using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
using ValtechBaseLine.Common;
using ValtechBaseLine.Model.Components;
using ValtechBaseLine.RepositoryContract.Components;
using ValtechBaseLine.Web.CustomFilters;
using Constants = ValtechBaseLine.Common.Constants;


namespace ValtechBaseLine.Web.Controllers
{
    public class ContactUsController : Controller
    {
        private readonly SitecoreContext _sitecoreContext;
        private readonly IContactUsContract _contactUs;
        private readonly IEmailSender _emailContext;
       
       

        public ContactUsController(IContactUsContract contact, IEmailSender email)
        {
            _sitecoreContext = new SitecoreContext();
            _contactUs = contact;
            _emailContext = email;
            
           
        }

        public ActionResult ContactUsForm(ContactUsModel contactDetails)
        {
            if (contactDetails == null)
            {
                contactDetails = default(ContactUsModel);
                string str = System.Web.HttpContext.Current.Request.UserHostAddress;
                if (!string.IsNullOrWhiteSpace(RenderingContext.Current.Rendering.DataSource))
                {
                    contactDetails = _contactUs.GetContactUsDetails(RenderingContext.Current.Rendering.DataSource);

                } 
            }
            
            return View("~/Views/Component/ContactUsView.cshtml", contactDetails);
        }

        

        [ReCaptcha]
        [ValidateAntiForgeryToken()]
        public ActionResult ContactUs(ContactUsModel model, HttpPostedFileBase files, bool captchaValid)
        {
            ContactUsModel contactDetails = default(ContactUsModel);

            try
            {

                contactDetails = _contactUs.GetContactUsDetails(Constants.ContactUs);
                //bool captchaValid = true;

                if (captchaValid)
                {
                    if (ModelState.IsValid)
                    {

                        contactDetails.FirstName = model.FirstName; //
                        contactDetails.LastName = model.LastName; //
                        contactDetails.UserEmail = model.UserEmail; //
                        contactDetails.MailSubject = model.MailSubject; //
                        contactDetails.Message = model.Message; //
                        string fileName = string.Empty;
                        if (files != null)
                        {
                            contactDetails.FileName = Path.GetFileName(files.FileName);
                            contactDetails.FileStream = files.InputStream;
                        }

                        var emailDto = _contactUs.MappingContactUs(contactDetails);
                        emailDto = _emailContext.Send(emailDto);

                        Tracker.Current.CurrentPage.Session.Identify(model.UserEmail);
                        
                        contactDetails.IsSent = emailDto.IsSent;
                        contactDetails.FailureMessage = emailDto.FailureMessage;
                    }
                }
                else
                {
                    return Content(contactDetails.SiteMessages["InvalidCaptcha"]);
                }


            }
            catch (Exception exp)
            {
                contactDetails.FailureMessage = "There was some problem occurs!";
            }
            string url = Sitecore.Links.LinkManager.GetItemUrl(Sitecore.Context.Database.GetItem(Constants.ContactUsPage));
            return Redirect(url);
        }

       
    }
}