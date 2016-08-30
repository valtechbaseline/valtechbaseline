using Glass.Mapper.Sc;
using Sitecore.Mvc.Controllers;
using Sitecore.Mvc.Presentation;
using System.Web.Mvc;

using ValtechBaseLine.Model.Components;

namespace ValtechBaseLine.Web.Controllers.Common
{
    public class HeaderNavigationController : SitecoreController
    {
        readonly SitecoreContext _sitecoreContext = new SitecoreContext();
        // GET: /HeaderNavigation/

        public ActionResult HeaderNavigation()
        {
            var headerNavigation = _sitecoreContext.GetItem<IHeaderNavigationModel>(RenderingContext.Current.Rendering.DataSource);
            if (headerNavigation == null) return null;
            return View("~/Views/Common/HeaderNavigation.cshtml",headerNavigation);
        }
	}
}