using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Data.Items;
using Sitecore.Pipelines.GetTranslation;
using ValtechBaseLine.Search;
using Const = ValtechBaseLine.Common;

namespace ValtechBaseLine.Web.Custom
{
    public class LookupDictionatyItem
    {
        
         public virtual void Process(GetTranslationArgs args)
         {

             var dictionaryItem = Sitecore.Context.Database.SelectSingleItem(Const.Constants.TaxonomySourcePath +
                            "//*[@@templateid='" + Const.Constants.TaxonomyTemplateID + "' and @Key='" + args.Key + "']");
             if (dictionaryItem != null)
             {
                 args.CustomData["item"] = dictionaryItem;
             }

         }

        /*

        /// <summary>
        // Results are fetched by Indexing
        /// </summary>
        /// <param name="args"></param>
        public virtual void Process(GetTranslationArgs args)
        {
            //var root = args.ContentDatabase.GetItem("/sitecore/content/Global Settings/Taxonomy");
            SearchIndex search = new SearchIndex();
            var root = search.GetTaxonomyList(args.Key);
            if (root != null && root.Count > 0)
            {
                Sitecore.Data.Items.Item item = Sitecore.Context.Database.GetItem(root[0].ItemId);
                if (item != null)
                {
                    args.CustomData["item"] = item;
                }

            }

        }
         */

    }
}