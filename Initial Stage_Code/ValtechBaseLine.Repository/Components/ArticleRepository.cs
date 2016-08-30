using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Glass.Mapper.Sc;
using ValtechBaseLine.Model.Components;
using ValtechBaseLine.Model.Search;
using ValtechBaseLine.RepositoryContract.Components;
using ValtechBaseLine.Search;

namespace ValtechBaseLine.Repository.Components
{
    public class ArticleRepository:IArticleContract
    {
        private readonly SitecoreContext _sitecoreContext;
        private SearchIndex _searchIndex;
         /// <summary>
        /// Default Constructor
        /// Instantiates the Sitecore service
        /// </summary>
        public ArticleRepository()
        {
            _sitecoreContext = new SitecoreContext();
           
        }

        public List<Articles> ArticleList(string datasource)
        {
            _searchIndex = new SearchIndex();
            return _searchIndex.GetArticlesByCategory(datasource);
        }
    }
}
