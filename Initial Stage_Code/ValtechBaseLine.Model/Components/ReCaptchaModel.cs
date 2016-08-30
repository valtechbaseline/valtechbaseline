using Glass.Mapper.Sc.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValtechBaseLine.Model.Common;

namespace ValtechBaseLine.Model.Components
{
   
    [SitecoreType(AutoMap = true)]
    public class ReCaptchaModel
    {
        [SitecoreField("IncludeJsScript")]
        public virtual string IncludeJsScript { get; set; }

         [SitecoreField("IncludePageScript")]
        public virtual string IncludePageScript { get; set; }

        [SitecoreField("GoogleDomain")]
        public virtual string GoogleDomain { get; set; }

         [SitecoreField("PublicKey")]
        public virtual string PublicKey { get; set; }

        [SitecoreField("PrivateKey")]
        public virtual string PrivateKey { get; set; }

        [SitecoreField("ResponseLink")]
        public virtual string ResponseLink { get; set; }
    }
}
