using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValtechBaseLine.Common.DTO;

namespace ValtechBaseLine.Data.EF
{
    public partial class ValtechBaseLineEntities : IValtechBaseLineEntities
    {
        private ValtechBaseLineEntities _context;


        public IValtechBaseLineEntities GetContext()
        {
            _context = new ValtechBaseLineEntities();  
            return _context;
        }

        public UserDTO GetFirstUser()
        {
            var dataBaseUser = new ValtechBaseLineEntities().Users.FirstOrDefault();
            if (dataBaseUser != null)
                return new UserDTO
                {
                    FirstName = dataBaseUser.FirstName,
                    LastName = dataBaseUser.LastName
                };
            return null;
        }

        public bool SaveUserInfo(UserDTO userDto)
        {
            var context = (ValtechBaseLineEntities)GetContext();
            User newUser = context.Users.Add(new User()
              {
                  FirstName = userDto.FirstName,
                  LastName = userDto.LastName,
                  PhoneNumber = userDto.PhoneNumber,
                  ScSalutationId = userDto.ScSalutationId,
                  ScCustId = userDto.ScCustId
              });

            if (newUser != null)
            {
                context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
