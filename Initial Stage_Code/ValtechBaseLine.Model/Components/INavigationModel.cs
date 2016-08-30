
using Glass.Mapper.Sc.Configuration.Attributes;
using Glass.Mapper.Sc.Fields;

namespace ValtechBaseLine.Model.Components
{
    public interface INavigationModel
    {
        [SitecoreField("NavigationName")]
        string NavigationName { get; set; }

        [SitecoreField("NavigationLink")]
        Link NavigationLink { get; set; }

        [SitecoreField("Tooltip")]
        string Tooltip { get; set; }

    }
}
