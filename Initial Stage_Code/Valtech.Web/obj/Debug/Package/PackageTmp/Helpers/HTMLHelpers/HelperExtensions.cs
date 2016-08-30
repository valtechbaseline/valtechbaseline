using Sitecore;
using Sitecore.Data;
using Sitecore.Globalization;
using Sitecore.Mvc.Helpers;
using Sitecore.Pipelines;
using Sitecore.Pipelines.GetTranslation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ValtechBaseLine.Search;
using Const=ValtechBaseLine.Common;

namespace ValtechBaseLine.Web.Helpers
{
    public static class HelperExtensions
    {
       

        public static IHtmlString Translation(this SitecoreHelper helper, string key)
        {
            if (Context.PageMode.IsPageEditorEditing)
            {
                return RenderEditablePhrase(key);
            }


            return new HtmlString(GetDictionaryKeyValue(key));
        }

        public  static string GetDictionaryKeyValue(string Key)
        {
            var dictionaryItem = Sitecore.Context.Database.SelectSingleItem(Const.Constants.TaxonomySourcePath +
                            "//*[@@templateid='" + Const.Constants.TaxonomyTemplateID + "' and @Key='" + Key + "']");

            if (dictionaryItem != null)
            {
                return dictionaryItem["Phrase"] != string.Empty ? dictionaryItem["Phrase"] : Key;
            }
            else
            {
                return Key;
            }
        }


        private static IHtmlString RenderEditablePhrase(string key)
        {
            var args = new GetTranslationArgs()
            {
                ContentDatabase = Context.ContentDatabase ?? Context.Database ?? Database.GetDatabase("core"),
                Key = key
            };

            CorePipeline.Run("valtechbaseline.translation.editable", args);

            return new HtmlString(args.Result);
        }

    }

    
}