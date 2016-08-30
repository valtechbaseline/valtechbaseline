using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Glass.Mapper.Sc.Configuration.Attributes;

namespace ValtechBaseLine.Model.Common
{
     [SitecoreType(AutoMap = true)]
    public class TrackUserModelCollection
    {
        public List<TrackUserModel> TrackUserList =new List<TrackUserModel>();
    }
}
