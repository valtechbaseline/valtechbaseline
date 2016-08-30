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
    public class GalleryRepository:IGalleryContract
    {
        private readonly SitecoreContext _sitecoreContext;

         /// <summary>
        /// Default Constructor
        /// Instantiates the Sitecore service
        /// </summary>
        public GalleryRepository()
        {
            _sitecoreContext = new SitecoreContext();
            
        }

        public IGalleryModel GetGallary(string datasource)
        {
            return this._sitecoreContext.GetItem<IGalleryModel>(new Guid(datasource));
        }
    }
}
