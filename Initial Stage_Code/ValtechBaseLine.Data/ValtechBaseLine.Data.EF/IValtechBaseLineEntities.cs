using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValtechBaseLine.Common.DTO;

namespace ValtechBaseLine.Data.EF
{
    public interface IValtechBaseLineEntities : IDisposable
    {
        IValtechBaseLineEntities GetContext();
        UserDTO GetFirstUser();
        bool SaveUserInfo(UserDTO userDto);
    }
}
