using System;
using Sitecore.ContentSearch;
using Sitecore.ContentSearch.ComputedFields;
using Sitecore.Data.Fields;
using Sitecore.Diagnostics;
using Sitecore.Resources.Media;

namespace ValtechBaseLine.Search.ComputedFields
{
    public class ImageComputedFields : IComputedIndexField
    {
        public string FieldName { get; set; }
        public string ReturnType { get; set; }

        public object ComputeFieldValue(IIndexable indexable)
        {
            Assert.ArgumentNotNull(indexable, "indexable");
            Sitecore.ContentSearch.SitecoreIndexableItem scIndexable =
            indexable as Sitecore.ContentSearch.SitecoreIndexableItem;

            if (scIndexable == null)
            {
                return null;
            }

            Sitecore.Data.Items.Item item = (Sitecore.Data.Items.Item)scIndexable;

            if (item == null
              || string.Compare(item.Database.Name, "core", StringComparison.OrdinalIgnoreCase) == 0
              || string.IsNullOrEmpty(this.FieldName)
              || string.IsNullOrEmpty(item[this.FieldName]))
            {
                return null;
            }


            if (item != null)
            {
                ImageField field = item.Fields[FieldName];
                if (field != null)
                {
                    var mediaPath = string.Empty;
                    if (field.MediaItem != null)
                    {
                        var mediaUrlOptions = new MediaUrlOptions { AbsolutePath = false, AlwaysIncludeServerUrl = false };

                        return Sitecore.StringUtil.EnsurePrefix('/', MediaManager.GetMediaUrl(field.MediaItem, mediaUrlOptions));

                    }
                }
            }
            return item;

        }
    }
}


