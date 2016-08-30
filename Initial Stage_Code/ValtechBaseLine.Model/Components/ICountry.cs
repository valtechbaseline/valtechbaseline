//----------------------------------------------------------------------
// <copyright file="ICountry.cs" campany="Valtech">
//  Copyright © 2016 by Valtech_. All rights are reserved.
//  Unauthorized copying of this file, via any medium is strictly prohibited. 
// </copyright>
// <author>IN\balasubrahmanyam.b</author>
// <time>4/11/2016 11:47:03 AM</time>
//----------------------------------------------------------------------

namespace ValtechBaseLine.Model.Interfaces
{
    /// <summary>
    /// ILanguage
    /// </summary>
    public interface ICountry
    {
         string Name { get; set; }
         string Culture { get; set; }
         string Url { get; set; }
    }

    public class Country : ICountry
    {

        public string Name { get; set; }
        public string Culture { get; set; }
        public string Url { get; set; }
    }
}
