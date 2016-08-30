//---------------------------------------------------------------------------
// <copyright file="LanguageSwitcher.cs" campany="Valtech">
//  Copyright © 2016 by Valtech_. All rights are reserved.
//  Unauthorized copying of this file, via any medium is strictly prohibited. 
// </copyright>
// <author>IN\balasubrahmanyam.b</author>
// <time>4/11/2016 11:54:19 AM</time>
//---------------------------------------------------------------------------

using Glass.Mapper.Sc;
using Sitecore.Collections;
using Sitecore.Globalization;
using Sitecore.Sites;
using Sitecore.Web;
using ValtechBaseLine.Model.Interfaces;
using ValtechBaseLine.RepositoryContract.Components;

namespace ValtechBaseLine.Repository.Components
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// MultiLanguages
    /// </summary>
    public class LanguageSwitcher:ILanguageSwitcher
    {
         private readonly SitecoreContext _sitecoreContext;

         /// <summary>
        /// Default Constructor
        /// Instantiates the Sitecore service
        /// </summary>
         public LanguageSwitcher()
        {
            _sitecoreContext = new SitecoreContext();
            
        }

         public List<ICountry> GetCountries()
         {
             List<ICountry> countries = new List<ICountry>();
             LanguageCollection languages = _sitecoreContext.Database.GetLanguages();
             foreach (Language language in languages)
             {
                 var country = new Country();
                 country.Name = language.CultureInfo.DisplayName;
                 country.Culture = language.CultureInfo.Name;
                 country.Url = "http://" + GetSite(language.Name).HostName;
                 countries.Add(country);
             }
             return countries;
         }

         private SiteInfo GetSite(string language)
         {
             return SiteContextFactory.Sites.First(s => s.Language == language && s.HostName != string.Empty);
         }
    }
}