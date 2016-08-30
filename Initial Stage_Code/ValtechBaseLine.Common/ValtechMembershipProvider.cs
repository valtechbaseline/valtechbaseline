using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;
using Sitecore.Security;
using Sitecore.Security.Accounts;
using ValtechBaseLine.Common;

namespace ValtechBaseLine.Common
{
    public class ValtechMembershipProvider
    {
        /// <summary>
        /// Creates user and isApproved property set to false.
        /// </summary>
        /// <param name="emailId"></param>
        /// <param name="password"></param>
        public string CreateUser(string emailId, string password, string fullName)
        {
            if (emailId == null || password == null) return string.Empty;

            MembershipCreateStatus status;
            var uniqueName = Guid.NewGuid().ToString("N").ToUpper();
            var userName = string.Concat("extranet", "\\", emailId); //Changed from uniquename to emailId
            var user = Membership.CreateUser(userName, password, emailId, null, null, true, out status);
            if (status == MembershipCreateStatus.Success && user!=null)
            {
                UserProfile userProfile = User.FromName(userName, true).Profile;
                UpdateUserProfile(userName, "{AE4C4969-5B7E-4B4E-9042-B2D8701CE214}", fullName);
                return uniqueName;
            }
            if (status == MembershipCreateStatus.DuplicateUserName)
            {
                return Constants.DuplicateEmail;
            }
            return string.Empty;
        }
        public void UpdateUserProfile(string userName, string profileId, string fullName)
        {
            User user = Sitecore.Security.Accounts.User.FromName(userName, true);
            user.Profile.ProfileItemId = profileId;
            user.Profile.FullName = fullName;
            user.Profile.Save();
        }
    }
}
