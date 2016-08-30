using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValtechBaseLine.Model.Components;
using ValtechBaseLine.Model.Search;

namespace ValtechBaseLine.RepositoryContract.Components
{
    public interface IArticleContract
    {
        List<Articles> ArticleList(string datasource);
    }
}
