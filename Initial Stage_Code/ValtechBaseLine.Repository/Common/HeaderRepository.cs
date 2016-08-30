using System;
using System.ComponentModel;
using Glass.Mapper.Sc;
using ValtechBaseLine.Data.EF;
using ValtechBaseLine.Model.Components;
using ValtechBaseLine.RepositoryContract.Common;



namespace ValtechBaseLine.Repository.Common
{
    public class HeaderRepository : IHeaderContract
    {
     
        private readonly SitecoreContext _sitecoreContext;
        /// <summary>
        /// Default Constructor
        /// Instantiates the Sitecore service
        /// </summary>
        public HeaderRepository()
        {
         
            _sitecoreContext = new SitecoreContext();
        }

        public IHeaderModel GetHeaderDetails(string datasource)
        {

            var header = this._sitecoreContext.GetItem<IHeaderModel>(new Guid(datasource));
       
            return header;
        }
    }
}
