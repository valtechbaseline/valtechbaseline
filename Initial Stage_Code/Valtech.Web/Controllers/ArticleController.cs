using System.Collections.Generic;
using System.Web.Mvc;
using Glass.Mapper.Sc;
using Sitecore.Mvc.Presentation;
using ValtechBaseLine.Model.Components;
using ValtechBaseLine.Model.Search;
using ValtechBaseLine.RepositoryContract.Components;

namespace ValtechBaseLine.Web.Controllers
{
    public class ArticleController : Controller
    {
        private readonly SitecoreContext _sitecoreContext;
        private readonly IArticleContract _article;  
        public ArticleController(IArticleContract articleContract)
        {
            _sitecoreContext = new SitecoreContext();
            _article = articleContract;
        }

        public ActionResult GetArticleList()
        {
            List<Articles> articleModel = default(List<Articles>);
            if (!string.IsNullOrWhiteSpace(RenderingContext.Current.Rendering.DataSource))
            {
                articleModel = _article.ArticleList(RenderingContext.Current.Rendering.DataSource);
            }
            return View("~/Views/Component/ArticleListView.cshtml" , articleModel);
        }
    }
}