using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Glass.Mapper.Sc;
using ValtechBaseLine.Model.Components;
using ValtechBaseLine.RepositoryContract.Components;

namespace ValtechBaseLine.Repository.Components
{
    public class TeaserRepository:ITeaserContract
    {
        private readonly SitecoreContext _sitecoreContext;

         /// <summary>
        /// Default Constructor
        /// Instantiates the Sitecore service
        /// </summary>
        public TeaserRepository()
        {
            _sitecoreContext = new SitecoreContext();
            
        }

        public IImageTeaserModel GetTeaser(string datasource)
        {
            return this._sitecoreContext.GetItem<IImageTeaserModel>(new Guid(datasource));
        }
    }
}
