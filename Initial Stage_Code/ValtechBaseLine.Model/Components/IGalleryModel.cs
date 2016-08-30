using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Glass.Mapper.Sc.Configuration.Attributes;
using ValtechBaseLine.Model.Common;

namespace ValtechBaseLine.Model.Components
{
    public interface IGalleryModel:ITitleModel
    {
        [SitecoreField("Number of rows")]
        string NumberOfrows { get; set; }

        [SitecoreField("Item List")]
        IEnumerable<IVideoTeaserModel> VideoList { get; set; }

        [SitecoreField("Item List")]
        IEnumerable<IImageTeaserModel> ImageList { get; set; }

    }
}
