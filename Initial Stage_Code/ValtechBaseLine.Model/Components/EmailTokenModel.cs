using Glass.Mapper.Sc.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValtechBaseLine.Model.Components
{
     [SitecoreType(AutoMap = true)]
    public class EmailTokenModel
    {
        [SitecoreField("Key")]
        public virtual string Key { get; set; }

        [SitecoreField("Phrase")]
        public virtual string Phrase { get; set; }

    }
}
