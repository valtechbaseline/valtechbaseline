using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Glass.Mapper.Sc.Configuration.Attributes;
using Glass.Mapper.Sc.Fields;

namespace ValtechBaseLine.Model.Common
{
    public interface ICtaModel
    {
        [SitecoreField("Primary Link")]
        Link PrimaryLink { get; set; }

        [SitecoreField("Secondary Link")]
        Link SecondaryLink { get; set; }
    }
}
