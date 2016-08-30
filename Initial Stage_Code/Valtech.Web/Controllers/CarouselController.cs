//---------------------------------------------------------------------------
// <copyright file="CarouselController.cs" campany="Valtech">
//  Copyright © 2016 by Valtech_. All rights are reserved.
//  Unauthorized copying of this file, via any medium is strictly prohibited. 
// </copyright>
// <author>IN\abhay.dhar</author>
// <time>5/3/2016 11:34:44 AM</time>
//---------------------------------------------------------------------------

using Glass.Mapper.Sc;
using Sitecore.Mvc.Presentation;
using ValtechBaseLine.Model.Components;
using ValtechBaseLine.RepositoryContract.Components;

namespace ValtechBaseLine.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    /// <summary>
    /// CarouselController
    /// </summary>
    public class CarouselController : Controller
    {
        private readonly SitecoreContext _sitecoreContext;
        private readonly ICarouselContract _carousel;   

        public CarouselController(ICarouselContract carouselContract)
        {
            _sitecoreContext = new SitecoreContext();
            _carousel = carouselContract;
        }

        public ActionResult Index()
        {
            CarouselModel carouselModel = default (CarouselModel);
            if (!string.IsNullOrWhiteSpace(RenderingContext.Current.Rendering.DataSource))
            {
                carouselModel = _carousel.CarouselDetails(RenderingContext.Current.Rendering.DataSource);
            }
            return View("~/Views/Component/CarouselView.cshtml" , carouselModel);
        }

    }
}
