using Glass.Mapper.Sc.Configuration.Attributes;
using System.Collections.Generic;
using System.IO;
using Sitecore.Data.Items;
using System.Collections.Specialized;
using Glass.Mapper.Sc.Fields;


namespace ValtechBaseLine.Model.Common
{
    [SitecoreType(AutoMap = true)]
    public class TrackUserModel
    {
        public string ItemUrl { get; set; }
        public string PageImageUrl { get; set; }
        public string Headline { get; set; }
        public string ShortDescription { get; set; }
        public string VisitorMarquee { get; set; }


    }
}
