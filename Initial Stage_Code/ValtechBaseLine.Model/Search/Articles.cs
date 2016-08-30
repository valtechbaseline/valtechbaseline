//---------------------------------------------------------------------------
// <copyright file="Articles.cs" campany="Valtech">
//  Copyright © 2016 by Valtech_. All rights are reserved.
//  Unauthorized copying of this file, via any medium is strictly prohibited. 
// </copyright>
// <author>IN\balasubrahmanyam.b</author>
// <time>5/11/2016 5:04:38 PM</time>
//---------------------------------------------------------------------------

using System.Collections.Generic;
using Glass.Mapper.Sc.Configuration.Attributes;
using Sitecore.ContentSearch;
using Sitecore.ContentSearch.SearchTypes;

namespace ValtechBaseLine.Model.Search
{

    /// <summary>
    /// Articles
    /// </summary>
    [SitecoreType]
    public class Articles : SearchResultItem
    {
        [IndexField("Description")]
        public string Description { get; set; }
        [IndexField("Heading")]
        public string Heading { get; set; }
        [IndexField("Sub Heading")]
        public string SubHeading { get; set; }

        [IndexField("urllink")]
        public string Urllink { get; set; }

        [IndexField("Image")]
        public string ImageUrl { get; set; }

        [IndexField("categoryguid")]
        public List<string> Category { get; set; }

        [IndexField("stateguid")]
        public string State { get; set; }

        [IndexField("subheading_upper")]
        public string SubheadingUpper { get; set; }

        [IndexField("heading_upper")]
        public string HeadingUpper { get; set; }

        [IndexField("Description_Upper")]
        public string DescriptionUpper { get; set; }

        [IndexField("__display_name")]
        public string displayname { get; set; }

        [IndexField("_latestversion")]
        public bool latestversion { get; set; }

        public string SearchedValue { get; set; }
        
    }
}