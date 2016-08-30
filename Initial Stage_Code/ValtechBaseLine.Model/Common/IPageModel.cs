using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Glass.Mapper.Sc.Configuration.Attributes;
using Glass.Mapper.Sc.Fields;

namespace ValtechBaseLine.Model.Common
{
    public interface IPageModel:IBaseModel
    {
        [SitecoreField("Browser Title")]
        string BrowserTitle { get; set; }

        [SitecoreField("Breadcrumb Title")]
        string BreadcrumbTitle { get; set; }

        [SitecoreField("Headline")]
        string Headline { get; set; }

        [SitecoreField("Short Description")]
        string ShortDescription { get; set; }

        [SitecoreField("Long Description")]
        string LongDescription { get; set; }

        [SitecoreField("Background Image")]
        Image BackgroundImage { get; set; }

        string Url { get; set; }
    }
}
