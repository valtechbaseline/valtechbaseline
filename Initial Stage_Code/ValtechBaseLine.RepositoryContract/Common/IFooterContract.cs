//---------------------------------------------------------------------------
// <copyright file="IForgotPassword.cs" campany="Valtech">
//  Copyright © 2016 by Valtech_. All rights are reserved.
//  Unauthorized copying of this file, via any medium is strictly prohibited. 
// </copyright>
// <author>IN\abhay.dhar</author>
// <time>4/5/2016 6:18:01 PM</time>
//---------------------------------------------------------------------------

using ValtechBaseLine.Model.Components;

namespace ValtechBaseLine.RepositoryContract.Common
{

    /// <summary>
    /// IForgotPassword
    /// </summary>
    public interface IFooterContract
    {
        IFooterModel GetFooterDetails(string datasource);
        NewsLetterSignupModel GetNewsLetterDetails(string datasource);
    }
}