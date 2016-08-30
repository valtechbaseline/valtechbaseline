using Glass.Mapper.Sc.Configuration.Attributes;
using Glass.Mapper.Sc.Fields;
using ValtechBaseLine.Model.Common;

namespace ValtechBaseLine.Model.Components
{
    [SitecoreType]
    public interface IHeaderModel:IBaseModel
    {
        [SitecoreField("HeaderTitle")]
        string HeaderTitle { get; set; }

        [SitecoreField("Header Logo")]
        Image HeaderLogo { get; set; }
    }

    
}
