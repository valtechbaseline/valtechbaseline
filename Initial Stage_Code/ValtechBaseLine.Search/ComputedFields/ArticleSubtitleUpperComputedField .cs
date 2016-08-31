﻿using System;
using Sitecore.ContentSearch;
using Sitecore.ContentSearch.ComputedFields;
using Sitecore.ContentSearch.Diagnostics;
using Sitecore.Data.Items;
using Sitecore.Links;
using ValtechBaseLine.Common;

namespace ValtechBaseLine.Search.ComputedFields
{
    public class ArticleSubtitleUpperComputedField : IComputedIndexField
    {
        public string FieldName { get; set; }
        public string ReturnType { get; set; }

        /// <summary>
        /// Computed field to compute values of towns field.
        /// </summary>
        /// <param name="indexable">indexable.</param>
        /// <returns>object.</returns>
        public object ComputeFieldValue(IIndexable indexable)
        {
            Item scIndexable = null;
            try
            {
                scIndexable = indexable as SitecoreIndexableItem;

                if (scIndexable == null)
                {
                    CrawlingLog.Log.Warn(
                        this + " : unsupported IIndexable type : " + indexable.GetType());
                    return false;
                }

                // optimization to reduce indexing time
                // by skipping this logic for items in the Core database
                if (
                    System.String.Compare(scIndexable.Database.Name, "core", System.StringComparison.OrdinalIgnoreCase) ==
                    0)
                {
                    return false;
                }
                string articleTemplateId = Constants.Templates.ArticleTemplateId; //Change To-do move to app settings

                if (articleTemplateId.ToUpper().Contains(scIndexable.TemplateID.ToString().ToUpper()))
                {
                    return scIndexable.Fields["Sub Heading"].ToString().ToUpper();
                }

            }
            catch (Exception ex)
            {
                string itemId = string.Empty;
                if (scIndexable != null)
                    itemId = scIndexable.ID.ToString();
                CrawlingLog.Log.Error(this.GetType().Name + " - Item ID: " + itemId, ex);
            }
            return null;
        }
    }
}