using System;
using Glass.Mapper.Sc;
using ValtechBaseLine.Model.Interfaces;
using ValtechBaseLine.RepositoryContract.Common;



namespace ValtechBaseLine.Repository.Common
{
    public class ContactUs : IContactUs
    {

        private readonly SitecoreContext _sitecoreContext;
        /// <summary>
        /// Default Constructor
        /// Instantiates the Sitecore service
        /// </summary>
        public ContactUs()
        {
            _sitecoreContext = new SitecoreContext();

        }

        public IBaseModel GetHeaderDetails(string datasource)
        {
            return this._sitecoreContext.GetItem<IBaseModel>(new Guid(datasource));

        }
    }
}
