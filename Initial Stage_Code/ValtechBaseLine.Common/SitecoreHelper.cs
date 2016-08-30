using Glass.Mapper.Sc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValtechBaseLine.Model.Common;

namespace ValtechBaseLine.Common
{
    public static class SitecoreHelper
    {
        private static SitecoreContext _sitecoreContext;
        static SitecoreHelper()
        {
            _sitecoreContext = new SitecoreContext();

        }
        public static string GetDictionaryValueByKey(string key)
        {
            var dictionaryItem = Sitecore.Context.Database.SelectSingleItem(Constants.TaxonomySourcePath +
                            "//*[@@templateid='" + Constants.TaxonomyTemplateID + "' and @Key='" + key + "']");

            if (dictionaryItem != null)
            {
                return dictionaryItem["Phrase"] != string.Empty ? dictionaryItem["Phrase"] : key;
            }
            return key;
        }

        public static ISiteSettingModel GetSiteSetting()
        {
            return _sitecoreContext.GetItem<ISiteSettingModel>(Constants.SiteSettings);
        }
    }
}
