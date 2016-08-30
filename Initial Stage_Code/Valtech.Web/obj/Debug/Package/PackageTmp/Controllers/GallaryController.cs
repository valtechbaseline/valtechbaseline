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
    public class GallaryController : Controller
    {
        private readonly SitecoreContext _sitecoreContext;
        private readonly IGalleryContract _myClass;
        //private readonly IEmailSender _email;

        //public TeaserController()
        //{
        //    _myClass = new TeaserRepository();
        //}

        public GallaryController(IGalleryContract myClass)
        {
            _sitecoreContext = new SitecoreContext();
            _myClass = myClass;
        }

        public ActionResult GetGallary()
        {
            IGalleryModel galleryDetails = default(IGalleryModel);
            if (!string.IsNullOrWhiteSpace(RenderingContext.Current.Rendering.DataSource))
            {
                galleryDetails = _myClass.GetGallary(RenderingContext.Current.Rendering.DataSource);
            }
            return View("~/Views/Component/GallaryView.cshtml", galleryDetails);

        }
	}
}