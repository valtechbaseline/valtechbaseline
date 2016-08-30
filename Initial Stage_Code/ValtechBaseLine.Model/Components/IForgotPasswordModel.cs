//---------------------------------------------------------------------------
// <copyright file="IForgotPassword.cs" campany="Valtech">
//  Copyright © 2016 by Valtech_. All rights are reserved.
//  Unauthorized copying of this file, via any medium is strictly prohibited. 
// </copyright>
// <author>IN\abhay.dhar</author>
// <time>4/5/2016 6:18:01 PM</time>
//---------------------------------------------------------------------------

namespace ValtechBaseLine.Model.Components
{
    using Glass.Mapper.Sc.Configuration.Attributes;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using ValtechBaseLine.Model.Common;

    /// <summary>
    /// IForgotPassword
    /// </summary>
    public interface IForgotPasswordModel:ITitleModel
    {
        [SitecoreField("Email Placeholder Text")]
        string EmailPlaceholderText { get; set; }

        [SitecoreField("Send Email Button Text")]
        string SendEmailButtonText { get; set; }

        [SitecoreField("Cancel Text")]
        string Cancel { get; set; }

    }
}