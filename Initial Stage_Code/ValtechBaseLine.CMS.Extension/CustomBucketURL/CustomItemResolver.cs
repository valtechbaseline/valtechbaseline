using System.Linq;
using Sitecore;
using Sitecore.Buckets.Managers;
using Sitecore.ContentSearch;
using Sitecore.ContentSearch.SearchTypes;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Data.Managers;
using Sitecore.Pipelines.HttpRequest;

namespace ValtechBaseLine.CMS.Extension.CustomBucketURL
{
    public class CustomItemResolver : HttpRequestProcessor
    {
        private const string ArticlesIndexDb = "sitecore_articlesindex_index";
        
        public override void Process(HttpRequestArgs args)
        {
            
            if (Context.Item == null)
            {
                var requestUrl = args.Url.ItemPath;

                // remove last element from path and see if resulting pathis a bucket
                var index = requestUrl.LastIndexOf('/');
                if (index > 0)
                {
                    var itemName = requestUrl.Substring(index + 1);
                    var indexdb = ContentSearchManager.GetIndex(ArticlesIndexDb);
                    // locate item in bucket by name
                    if (Sitecore.Context.Database == null)
                        return;
                    ID contextLanguageId = LanguageManager.GetLanguageItemId(Sitecore.Context.Language, Sitecore.Context.Database);
                    Item contextLanguage=null;
                    using (var searchContext = indexdb.CreateSearchContext())
                    {
                        
                        if (!contextLanguageId.IsNull)
                        {
                            contextLanguage = Context.Database.GetItem(contextLanguageId);

                        }
                        if (contextLanguage != null)
                        {
                            var result = searchContext
                                .GetQueryable<CustomBucketItem>()
                                .FirstOrDefault(
                                    x =>
                                        (x.latestversion) && (x.Language == contextLanguage.Name) &&
                                        (x.displayname == itemName || x.Name == itemName));
                            if (result != null)
                                Context.Item = result.GetItem();
                        }

                        else
                        {
                            var result = searchContext
                           .GetQueryable<CustomBucketItem>().FirstOrDefault(x => (x.latestversion) && (x.displayname == itemName || x.Name == itemName));
                            if (result != null)
                                Context.Item = result.GetItem();
                        }
                       
                    }
                }


            }


        }
    }

    public class CustomBucketItem : SearchResultItem
    {
        [IndexField("__display_name")]
        public string displayname { get; set; }

        [IndexField("_latestversion")]
        public bool latestversion { get; set; }
    }
}

