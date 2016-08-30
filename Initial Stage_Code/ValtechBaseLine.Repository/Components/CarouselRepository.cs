using Glass.Mapper.Sc;
using System;
using ValtechBaseLine.Model.Components;
using ValtechBaseLine.RepositoryContract.Components;

namespace ValtechBaseLine.Repository.Components
{
    public class CarouselRepository : ICarouselContract
    {
        private readonly SitecoreContext _sitecoreContext;

         /// <summary>
        /// Default Constructor
        /// Instantiates the Sitecore service
        /// </summary>
        public CarouselRepository()
        {
            _sitecoreContext = new SitecoreContext();
        }

        public CarouselModel CarouselDetails(string datasource)
        {
            return this._sitecoreContext.GetItem<CarouselModel>(new Guid(datasource));
        }
    }
}
