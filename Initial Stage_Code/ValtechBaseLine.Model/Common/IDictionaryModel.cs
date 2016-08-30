using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Glass.Mapper.Sc.Configuration.Attributes;
using Glass.Mapper.Sc.Fields;

namespace ValtechBaseLine.Model.Common
{
    public interface IDictionaryModel :IBaseModel
    {
        [SitecoreField("Key")]
        string Key { get; set; }

        [SitecoreField("Phrase")]
        string Phrase { get; set; }


    }
}
