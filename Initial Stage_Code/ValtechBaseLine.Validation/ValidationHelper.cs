using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValtechBaseLine.Validation
{
    static class ValidationHelper
    {
        public static string GetDictionaryValueByKey(string key)
        {
            var dictionaryItem = Sitecore.Context.Database.SelectSingleItem(ValidationConstants.TaxonomySourcePath +
                            "//*[@@templateid='" + ValidationConstants.TaxonomyTemplateID + "' and @Key='" + key + "']");

            if (dictionaryItem != null)
            {
                return dictionaryItem["Phrase"] != string.Empty ? dictionaryItem["Phrase"] : key;
            }
            return key;
        }

    }
}
