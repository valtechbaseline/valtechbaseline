using System;
using System.Collections.Generic;
using System.Linq;
using Sitecore.ContentSearch;
using Sitecore.ContentSearch.ComputedFields;
using Sitecore.Diagnostics;

namespace ValtechBaseLine.Search.ComputedFields
{
    public class ArticleCategoryComputedField : IComputedIndexField
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
              || string.IsNullOrEmpty(this.FieldName))
            {
                return null;
            }


            if (item != null)
            {
                if (item.Fields["Category"] != null)
                {
                    List<string> categoryList =new List<string>();
                    categoryList = item.Fields["Category"].Value.Split('|').ToList();
                    categoryList.ForEach(i=> i = !string.IsNullOrEmpty(i)? new Guid(i).ToString():string.Empty);
                    if (categoryList != null && categoryList.Any())
                    {
                        return categoryList;
                    }
                }
            }
            return null;

        }
    }
}
