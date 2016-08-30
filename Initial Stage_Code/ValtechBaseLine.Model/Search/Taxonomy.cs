//---------------------------------------------------------------------------
// <copyright file="Taxonomy.cs" campany="Valtech">
//  Copyright © 2016 by Valtech_. All rights are reserved.
//  Unauthorized copying of this file, via any medium is strictly prohibited. 
// </copyright>
// <author>IN\balasubrahmanyam.b</author>
// <time>4/29/2016 2:32:52 PM</time>
//---------------------------------------------------------------------------

using Glass.Mapper.Sc.Configuration.Attributes;
using Sitecore.ContentSearch;
using Sitecore.ContentSearch.SearchTypes;

namespace ValtechBaseLine.Model.Search
{
    /// <summary>
    /// Taxonomy
    /// </summary>
    [SitecoreType]
    public class Taxonomy : SearchResultItem
    {
        [IndexField("Key")]
        public string Key { get; set; }
        [IndexField("Phrase")]
        public string Phrase { get; set; }
    }
}