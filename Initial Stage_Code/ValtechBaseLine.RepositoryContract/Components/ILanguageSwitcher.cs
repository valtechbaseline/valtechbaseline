//----------------------------------------------------------------------
// <copyright file="ILanguageSwitcher.cs" campany="Valtech">
//  Copyright © 2016 by Valtech_. All rights are reserved.
//  Unauthorized copying of this file, via any medium is strictly prohibited. 
// </copyright>
// <author>IN\balasubrahmanyam.b</author>
// <time>4/11/2016 11:42:59 AM</time>
//----------------------------------------------------------------------

using System.Collections.Generic;
using ValtechBaseLine.Model.Interfaces;

namespace ValtechBaseLine.RepositoryContract.Components
{
    /// <summary>
    /// IMultiLanguages
    /// </summary>
    public interface ILanguageSwitcher
    {

        List<ICountry> GetCountries();
    }
}
