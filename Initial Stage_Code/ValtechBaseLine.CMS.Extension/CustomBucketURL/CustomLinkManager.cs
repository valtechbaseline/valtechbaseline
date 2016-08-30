using Sitecore;
using Sitecore.Buckets.Extensions;
using Sitecore.Buckets.Managers;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Data.Managers;
using Sitecore.IO;
using Sitecore.Links;

namespace ValtechBaseLine.CMS.Extension.CustomBucketURL
{
    public class CustomLinkManager : LinkProvider
    {
       
        public override string GetItemUrl(Sitecore.Data.Items.Item item,
 UrlOptions options)
        {
            if (BucketManager.IsItemContainedWithinBucket(item))
            {
                var bucketItem = item.GetParentBucketItemOrParent();
                if (bucketItem != null && bucketItem.IsABucket())
                {
                    var bucketUrl = base.GetItemUrl(bucketItem, options);
                    bucketUrl = bucketUrl.Replace(options.Language.Name+ "/Valtech/", "");
                    if (options.AddAspxExtension)
                        bucketUrl = bucketUrl.Replace(".aspx", string.Empty);

                    return FileUtil.MakePath(bucketUrl, !string.IsNullOrEmpty(item.DisplayName)?item.DisplayName:item.Name) +
        (options.AddAspxExtension ? ".aspx" : string.Empty);
                }
            }

            return base.GetItemUrl(item, options);
        }
    }
}
