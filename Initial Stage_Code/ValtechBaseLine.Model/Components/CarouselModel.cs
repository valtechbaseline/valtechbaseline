//---------------------------------------------------------------------------
// <copyright file="CarouselModel.cs" campany="Valtech">
//  Copyright © 2016 by Valtech_. All rights are reserved.
//  Unauthorized copying of this file, via any medium is strictly prohibited. 
// </copyright>
// <author>IN\abhay.dhar</author>
// <time>5/3/2016 11:35:46 AM</time>
//---------------------------------------------------------------------------

using Glass.Mapper.Sc.Configuration.Attributes;
using Glass.Mapper.Sc.Fields;

namespace ValtechBaseLine.Model.Components
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    /// <summary>
    /// CarouselModel
    /// </summary>
    
    [SitecoreType(AutoMap = true)]
    public class CarouselModel
    {
        [SitecoreField("Carousel Items")]
        public IEnumerable<IImageTeaserModel> CarouselImages { get; set; }

        [SitecoreField("Carousel Items")]
        public IEnumerable<IVideoTeaserModel> CarouselVideos { get; set; }

        public string Marquee { get; set; }
    }
}