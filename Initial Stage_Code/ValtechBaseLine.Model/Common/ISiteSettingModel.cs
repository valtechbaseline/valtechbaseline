using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Glass.Mapper.Sc.Configuration.Attributes;
using Glass.Mapper.Sc.Fields;

namespace ValtechBaseLine.Model.Common
{
    public interface ISiteSettingModel
    {
        #region Google ReCaptcha

        [SitecoreField("Public Key")]
        string PublicKey { get; set; }

        [SitecoreField("Private Key")]
        string PrivateKey { get; set; }

        #endregion

        #region Search Section

        [SitecoreField("Search URL")]
        Link SearchUrl { get; set; }

        #endregion
    }
}
