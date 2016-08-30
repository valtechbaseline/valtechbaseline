using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Glass.Mapper.Sc.Configuration.Attributes;

namespace ValtechBaseLine.Model.Components
{
    [SitecoreType(AutoMap = true)]
    public class Deal
    {
        [SitecoreField("Deals Heading")]
        public virtual string DealsHeading { get; set; }  //Deals Heading

        [SitecoreField("Deals Selector")]
        public virtual IEnumerable<DealComponent> DealsList { get; set; }
    }
}
