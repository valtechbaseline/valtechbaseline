using System;
using Glass.Mapper.Sc;
using ValtechBaseLine.Model.Components;
using ValtechBaseLine.RepositoryContract.Common;

namespace ValtechBaseLine.Repository.Common
{
    public class FooterRepository : IFooterContract
    {
        private readonly SitecoreContext _sitecoreContext;

         /// <summary>
        /// Default Constructor
        /// Instantiates the Sitecore service
        /// </summary>
        public FooterRepository()
        {
            _sitecoreContext = new SitecoreContext();
            
        }

        public IFooterModel GetFooterDetails(string datasource)
        {
            var t= _sitecoreContext.GetItem<IFooterModel>(new Guid(datasource));
            return t;
        }

        public NewsLetterSignupModel GetNewsLetterDetails(string datasource)
        {
            var t = _sitecoreContext.GetItem<NewsLetterSignupModel>(new Guid(datasource));
            return t;
        }

    }
}
