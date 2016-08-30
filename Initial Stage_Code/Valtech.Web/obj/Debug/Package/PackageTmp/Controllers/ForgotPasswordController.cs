//---------------------------------------------------------------------------
// <copyright file="ForgotPassword.cs" campany="Valtech">
//  Copyright © 2016 by Valtech_. All rights are reserved.
//  Unauthorized copying of this file, via any medium is strictly prohibited. 
// </copyright>
// <author>IN\abhay.dhar</author>
// <time>4/5/2016 6:15:10 PM</time>
//---------------------------------------------------------------------------

namespace ValtechBaseLine.Web.Controllers
{
    using Glass.Mapper.Sc;
    using Sitecore.Mvc.Presentation;
    using System.Web.Mvc;
    using ValtechBaseLine.Model.Components;
    using ValtechBaseLine.RepositoryContract.Components;    

    /// <summary>
    /// ForgotPassword
    /// </summary>
    public class ForgotPasswordController : Controller
    {
        //
        // GET: /Test/
        private readonly SitecoreContext _sitecoreContext;
        private readonly IForgotPasswordContract _forgotPswd;

        public ForgotPasswordController(IForgotPasswordContract forgotPswd)
        {
            _sitecoreContext = new SitecoreContext();
            _forgotPswd = forgotPswd;
        }

        public ActionResult ForgotPassword()
        {
            IForgotPasswordModel forgotPswdModel = default(IForgotPasswordModel);
            if (!string.IsNullOrWhiteSpace(RenderingContext.Current.Rendering.DataSource))
            {
                forgotPswdModel = _forgotPswd.GetForgotPassword(RenderingContext.Current.Rendering.DataSource);
            }
            return View("~/Views/Component/ForgotPassword.cshtml",forgotPswdModel);
        }

    }
}
