using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using Glass.Mapper.Sc;
using Sitecore.Diagnostics;
using Sitecore.Globalization;
using ValtechBaseLine.RepositoryContract.Common;
using ValtechBaseLine.RepositoryContract.Components;

namespace ValtechBaseLine.Web.Controllers
{
    public class LanguageSwitcherController : Controller
    {
        //
        // GET: /Test/
        private readonly SitecoreContext _sitecoreContext;
        private readonly ILanguageSwitcher _languageSwitcher;
        

        public LanguageSwitcherController(ILanguageSwitcher languageSwitcher)
        {
            _sitecoreContext = new SitecoreContext();
            _languageSwitcher = languageSwitcher;
        }

        // GET: /LanguageSwitcher/
        public ActionResult LoadLanguages()
        {
            var countries=_languageSwitcher.GetCountries();
            var currentculture = Sitecore.Context.Culture.Name;
            var bindCountries = countries.Select(item => new SelectListItem { Value = item.Culture, Text = item.Name, Selected = item.Culture == currentculture }).ToList();
            return View("~/Views/Component/LanguageSwitcher.cshtml", bindCountries);
        }

        
        public JsonResult ChangeCurrentCulture(string culture)
        {
            Sitecore.Context.SetLanguage(Language.Parse(culture), true);
            return Json(new { Message = "Language Changed" ,Status = "Success" },JsonRequestBehavior.AllowGet);
        }

        
	}
}