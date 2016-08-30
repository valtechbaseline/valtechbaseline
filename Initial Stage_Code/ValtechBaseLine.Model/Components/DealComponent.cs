using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Glass.Mapper.Sc.Configuration.Attributes;
using Glass.Mapper.Sc.Fields;

namespace ValtechBaseLine.Model.Components
{
    [SitecoreType(AutoMap = true)]
    public class DealComponent
    {
        [SitecoreField("Heading")]
        public virtual string Heading { get; set; }

        [SitecoreField("Sub Heading")]
        public virtual string SubHeading { get; set; }

        [SitecoreField("Image")]
        public virtual Image DealImage { get; set; }

        [SitecoreField("Image Description")]
        public virtual string DealImageDescription { get; set; }
    }
}
