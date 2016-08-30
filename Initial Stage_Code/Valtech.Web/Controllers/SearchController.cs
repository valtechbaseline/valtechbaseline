using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Glass.Mapper.Sc;
using Sitecore.Mvc.Presentation;
using ValtechBaseLine.Common;
using ValtechBaseLine.Model.Common;
using ValtechBaseLine.RepositoryContract.Common;
using ValtechBaseLine.Search;

namespace ValtechBaseLine.Web.Controllers
{
    public class SearchController : Controller
    {
        private readonly IAnalyticsContract _analyticsContract;
        //
        // GET: /Search/

        public SearchController( IAnalyticsContract commonContract)//,IEmailSender email)
        {
            
            _analyticsContract = commonContract;
        }

        public ActionResult Index(string searchedValue)
        {
            var search=new SearchIndex();
            var result = search.GetArticlesList(searchedValue);
            
            return View("~/Views/Component/SearchView.cshtml",result);
        }

        public JsonResult SearchedArticleGoalTrigger()
        {
            _analyticsContract.TriggerGoal(Constants.Goals.SearchedArticle, "user has clicked on Article Page");
            var jsonData = new
            {
                Success = true,
                Message = "Successfully registered goal",

            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        } 
	}
}