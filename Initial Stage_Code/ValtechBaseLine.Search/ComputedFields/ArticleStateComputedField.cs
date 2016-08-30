using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore.ContentSearch;
using Sitecore.ContentSearch.ComputedFields;
using Sitecore.Diagnostics;

namespace ValtechBaseLine.Search.ComputedFields
{
    public class ArticleStateComputedField : IComputedIndexField
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
                if (item.Fields["State"] != null)
                {
                    string field =!string.IsNullOrEmpty(item.Fields["State"].Value)? new Guid(item.Fields["State"].Value).ToString():null;
                    if (field != null)
                    {
                        return field;
                    }
                }
            }
            return null;

        }
    }
}
