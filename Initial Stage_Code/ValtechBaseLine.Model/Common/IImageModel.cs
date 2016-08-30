using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Glass.Mapper.Sc.Configuration.Attributes;
using Glass.Mapper.Sc.Fields;

namespace ValtechBaseLine.Model.Common
{
    public interface IImageModel:IBaseModel
    {
        [SitecoreField("Image")]
        Image Image { get; set; }

        [SitecoreField("Image Description")]
        string ImageDescription { get; set; }
    }
}
