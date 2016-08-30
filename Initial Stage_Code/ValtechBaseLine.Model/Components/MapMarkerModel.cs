using Glass.Mapper.Sc.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValtechBaseLine.Model.Components
{
    [SitecoreType(AutoMap = true)]
    public class MapMarkerModel
    {
        [SitecoreField("title")]
        public virtual string title { get; set; }

        [SitecoreField("lat")]
        public virtual string lat { get; set; }

        [SitecoreField("lng")]
        public virtual string lng { get; set; }

        [SitecoreField("description")]
        public virtual string description { get; set; }
    }
}
