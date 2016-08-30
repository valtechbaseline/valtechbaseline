using System.Collections.Generic;
using Glass.Mapper.Sc.Configuration.Attributes;
using ValtechBaseLine.Model.Common;

namespace ValtechBaseLine.Model.Components
{
    public interface IHeaderNavigationModel : IBaseModel
    {
        [SitecoreField("HideNavigation")]
        bool HideNavigation { get; set; }

        [SitecoreField("Navigation Title")]
        string NavigationTitle { get; set; }

        [SitecoreChildren]
        IEnumerable<IHeaderNavigationModel> Children { get; set; }

        string Url { get; set; }
    }
}