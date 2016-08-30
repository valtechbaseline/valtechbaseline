//----------------------------------------------------------------------
// <copyright file="ITitle.cs" campany="Valtech">
//  Copyright © 2016 by Valtech_. All rights are reserved.
//  Unauthorized copying of this file, via any medium is strictly prohibited. 
// </copyright>
// <author>IN\abhay.dhar</author>
// <time>4/5/2016 6:24:32 PM</time>
//----------------------------------------------------------------------

namespace ValtechBaseLine.Model.Common
{
    using Glass.Mapper.Sc.Configuration.Attributes;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// ITitle
    /// </summary>
    public interface ITitleModel:IBaseModel
    {
        [SitecoreField("Heading")]
        string Heading { get; set; }

        [SitecoreField("Sub Heading")]
        string SubHeading { get; set; }
    }
}
