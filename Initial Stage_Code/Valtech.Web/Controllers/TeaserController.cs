using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Glass.Mapper.Sc;
using Sitecore.Mvc.Presentation;
using ValtechBaseLine.Model.Components;
using ValtechBaseLine.RepositoryContract.Components;

namespace ValtechBaseLine.Web.Controllers
{
    public class TeaserController : Controller
    {
        
         private readonly SitecoreContext _sitecoreContext;
        private readonly ITeaserContract _myClass;
        //private readonly IEmailSender _email;

        public TeaserController(ITeaserContract myClass)
        {
            _sitecoreContext = new SitecoreContext();
            _myClass = myClass;
        }

        public ActionResult GetTeaser()
        {
            IImageTeaserModel teaserDetails = default(IImageTeaserModel);
            if (!string.IsNullOrWhiteSpace(RenderingContext.Current.Rendering.DataSource))
            {
                teaserDetails = _myClass.GetTeaser(RenderingContext.Current.Rendering.DataSource);
            }
            return View("~/Views/Component/TeaserView.cshtml", teaserDetails);

        }
	}
}