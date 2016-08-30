//---------------------------------------------------------------------------
// <copyright file="SearchIndex.cs" campany="Valtech">
//  Copyright © 2016 by Valtech_. All rights are reserved.
//  Unauthorized copying of this file, via any medium is strictly prohibited. 
// </copyright>
// <author>IN\balasubrahmanyam.b</author>
// <time>4/29/2016 2:57:21 PM</time>
//---------------------------------------------------------------------------


using Sitecore.ContentSearch;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Data.Managers;
using ValtechBaseLine.Model.Search;

namespace ValtechBaseLine.Search
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using ValtechBaseLine.Common;

    /// <summary>
    /// SearchIndex
    /// </summary>
    public class SearchIndex
    {
        private const string TaxonomyIndexDb = "sitecore_taxonomy_index";
        private const string ArticlesIndexDb = "sitecore_articlesindex_index";
        ID contextLanguageId = LanguageManager.GetLanguageItemId(Sitecore.Context.Language, Sitecore.Context.Database);
        Item contextLanguage;
        string language;
        public SearchIndex()
        {
            contextLanguage = Sitecore.Context.Database.GetItem(contextLanguageId);
            language = contextLanguage.Language.ToString();
        }
        //public List<Taxonomy> GetTaxonomyList(string match)
        //{
        //    ID dictionaryTemplateId = new ID("E3C58E84-8E0D-4CF0-A25A-005E3ED9C6BC");
        //    var index = ContentSearchManager.GetIndex(TaxonomyIndexDb);
        //    List<Taxonomy> allTaxonomys= new List<Taxonomy>();

        //    using (IProviderSearchContext context = index.CreateSearchContext())
        //    {
        //        if (!string.IsNullOrEmpty(match))
        //        {
        //            allTaxonomys = context.GetQueryable<Taxonomy>()
        //                              .Where(s => s.TemplateId == dictionaryTemplateId && s.Key.Contains(match) && s.Language == contextLanguage.Name).AsParallel().ToList();
        //        }
        //    }
        //    allTaxonomys = allTaxonomys.ToList();
        //    return allTaxonomys;


        //}

        public List<Articles> GetArticlesList(string match)
        {
            ID dictionaryTemplateId = new ID(Constants.Templates.ArticleTemplateId);
            var index = ContentSearchManager.GetIndex(ArticlesIndexDb);
            List<Articles> allArticles= new List<Articles>();

            using (IProviderSearchContext context = index.CreateSearchContext())
            {
                if (!string.IsNullOrEmpty(match))
                {
                    allArticles = context.GetQueryable<Articles>()
                        .Where(s => s.TemplateId == dictionaryTemplateId &&
                                                  s.Language == contextLanguage.Name && s.latestversion &&( (s.SubheadingUpper.Contains(match.ToUpper())) || s.DescriptionUpper.Contains(match.ToUpper()) || s.HeadingUpper.Contains(match.ToUpper()))).ToList();
                   
                }
            }
            allArticles = allArticles.ToList();
            if(allArticles.Any())
            allArticles[0].SearchedValue = match;
            return allArticles;

        }


        public List<Articles> GetArticlesByCategory(string category)
        {
            ID dictionaryTemplateId = new ID(Constants.Templates.ArticleTemplateId);
            var index = ContentSearchManager.GetIndex(ArticlesIndexDb);
            List<Articles> allArticles = new List<Articles>();

            using (IProviderSearchContext context = index.CreateSearchContext())
            {
                if (!string.IsNullOrEmpty(category))
                {
                    //allArticles = context.GetQueryable<Articles>()
                    //    .Where(s => s.TemplateId == dictionaryTemplateId &&
                    //                              s.Language == contextLanguage.Name && s.latestversion).ToList();
                    //allArticles = allArticles.Where(s => s.Category.Contains(category)).ToList();
                    allArticles = context.GetQueryable<Articles>()
                        .Where(s => s.TemplateId == dictionaryTemplateId &&
                                                  s.Language == contextLanguage.Name && s.latestversion && s.Category.Contains(category)).ToList();

                }
            }
            return allArticles;
        }
       
    }
}