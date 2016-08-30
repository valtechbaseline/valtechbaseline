using Glass.Mapper.Sc.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValtechBaseLine.Model.Components
{
    [SitecoreType(AutoMap = true)]
    public class GoogleMapModel
    {
        [SitecoreField("GoogleMapScript")]
        public virtual string GoogleMapScript { get; set; }

        [SitecoreField("Latitude")]
        public virtual string Latitude { get; set; }

        [SitecoreField("Longitude")]
        public virtual string Longitude { get; set; }

        [SitecoreField("MarkerLocation")]
        public virtual IEnumerable<MapMarkerModel> MarkerLocation { get; set; }

        [SitecoreField("Zoom")]
        public virtual string Zoom { get; set; }

        [SitecoreField("Width")]
        public virtual string Width { get; set; }

        [SitecoreField("Height")]
        public virtual string Height { get; set; }

        [SitecoreField("GoogleDistanceScript")]
        public virtual string GoogleDistanceScript { get; set; }

        [SitecoreField("StartingPosition")]
        public virtual string StartingPosition { get; set; }

        [SitecoreField("DestinationPosition")]
        public virtual string DestinationPosition { get; set; }

        [SitecoreField("TravelMode")]
        public virtual string TravelMode { get; set; }


    }
}
