using Sitecore.Analytics;
using Sitecore.Configuration;
using Sitecore.Data;
using Sitecore.Modules.EmailCampaign;
using Sitecore.Modules.EmailCampaign.Core;
using Sitecore.Modules.EmailCampaign.Core.Pipelines.RedirectUrl;
using Sitecore.Modules.EmailCampaign.Exceptions;
using Sitecore.Modules.EmailCampaign.Factories;
using Sitecore.Modules.EmailCampaign.Recipients;
using Sitecore.Modules.EmailCampaign.UI;
using Sitecore.Modules.EmailCampaign.Xdb;
using System;


namespace ValtechBaseLine.CMS.Extension
{
    
    public class GuidCryptoServiceProviderDefault : GuidCryptoServiceProvider
    {
        public GuidCryptoServiceProviderDefault(System.Guid initializationVector)
            : base(System.Text.Encoding.UTF8.GetBytes(GlobalSettings.PrivateKey), initializationVector.ToByteArray())
        {
        }
    }
}
