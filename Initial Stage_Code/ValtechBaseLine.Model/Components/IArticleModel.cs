using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Glass.Mapper.Sc.Configuration.Attributes;
using Glass.Mapper.Sc.Fields;
using Sitecore.Data.Items;
using ValtechBaseLine.Model.Common;

namespace ValtechBaseLine.Model.Components
{
    public interface IArticleModel:ITitleModel,IImageModel
    {
        [SitecoreField("Headline")]
        string Headline { get; set; }

        [SitecoreField("Description")]
        string Description { get; set; }

        [SitecoreField("GoogleMapScript")]
        string GoogleMapScript { get; set; }

        [SitecoreField("Latitude")]
        string Latitude { get; set; }

        [SitecoreField("Longitude")]
        string Longitude { get; set; }

        [SitecoreField("Representative")]
        Link Representative { get; set; }
    }
}
