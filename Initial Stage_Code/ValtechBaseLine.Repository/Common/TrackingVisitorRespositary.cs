using System;
using System.ComponentModel;
using Glass.Mapper.Sc;
using ValtechBaseLine.CMS.Extension.TrackingVisitor;
using ValtechBaseLine.Data.EF;
using ValtechBaseLine.Model.Common;
using ValtechBaseLine.Model.Components;
using ValtechBaseLine.RepositoryContract.Common;


namespace ValtechBaseLine.Repository.Common
{
    public class TrackingVisitorRespositary : ITrackVisitor
    {
        private readonly SitecoreContext _sitecoreContext;
        /// <summary>
        /// Default Constructor
        /// Instantiates the Sitecore service
        /// </summary>
        public TrackingVisitorRespositary()
        {

            _sitecoreContext = new SitecoreContext();
        }

        public TrackUserModelCollection GetBrowserCookieValue()
        {
            TrackUserModelCollection trackCollection;
            using (var brcookie = new BrowsingCookie())
            {
                trackCollection = brcookie.GetBrowserCookieValue();
            }
            return trackCollection;
        }
    }
}
