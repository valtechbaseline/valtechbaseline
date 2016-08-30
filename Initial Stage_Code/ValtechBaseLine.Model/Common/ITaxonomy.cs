using Glass.Mapper.Sc.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValtechBaseLine.Model.Common
{
    public interface ITaxonomy : IBaseModel
    {
        [SitecoreField("Value")]
        string Value { get; set; }

        [SitecoreChildren]
        IEnumerable<ITaxonomy> Children { get; set; }
    }
}
