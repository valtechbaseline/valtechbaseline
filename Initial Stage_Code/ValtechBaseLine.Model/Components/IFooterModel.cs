using System.Collections.Generic;
using Glass.Mapper.Sc.Configuration.Attributes;
using ValtechBaseLine.Model;

namespace ValtechBaseLine.Model.Components
{
    public interface IFooterModel
    {
        [SitecoreField("Navigation")]
        IEnumerable<INavigationModel> NavigationLink { get; set; }
    }
}
