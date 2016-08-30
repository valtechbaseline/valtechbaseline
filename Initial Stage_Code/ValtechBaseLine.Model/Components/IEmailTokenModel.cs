using Glass.Mapper.Sc.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValtechBaseLine.Model.Components
{
    public interface IEmailTokenModel
    {
        [SitecoreField("Key")]
        string Key { get; set; }

        [SitecoreField("Phrase")]
        string Phrase { get; set; }

    }
}
