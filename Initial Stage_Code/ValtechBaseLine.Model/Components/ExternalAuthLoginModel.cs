using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValtechBaseLine.Model.Components
{
    public class ExternalAuthLoginModel
    {
        public string Email { get; set; }
        public string LoginProviderId { get; set; }
        public string ProviderAuthKey { get; set; }
        public string Name { get; set; }
    }
}
