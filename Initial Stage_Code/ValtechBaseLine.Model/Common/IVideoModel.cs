using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Glass.Mapper.Sc.Configuration.Attributes;
using Glass.Mapper.Sc.Fields;

namespace ValtechBaseLine.Model.Common
{
    public interface IVideoModel:IBaseModel
    {
        [SitecoreField("Video URL")]
        Link VideoLink { get; set; }

        [SitecoreField("Description")]
        String Description { get; set; }
    }
}
