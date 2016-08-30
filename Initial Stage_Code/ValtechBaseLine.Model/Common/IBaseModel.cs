using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Glass.Mapper.Sc.Configuration;
using Glass.Mapper.Sc.Configuration.Attributes;
using Sitecore.Data;

namespace ValtechBaseLine.Model.Common
{
    [SitecoreType]
    public interface IBaseModel
    {
        /// <summary>
        /// Gets or Sets Item ID.
        /// </summary>
        [SitecoreId]
        ID Id { get; set; }

        /// <summary>
        /// Gets or Sets DisplayName.
        /// </summary>
        [SitecoreField("__Display name")]
        string DisplayName { get; set; }

        /// <summary>
        /// Gets or Sets ItemName.
        /// </summary>
        [SitecoreInfo(SitecoreInfoType.Name)]
        string ItemName { get; set; }
    }
}
