using Ma3d.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ma3d.DTO.V1
{
    public class UserRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public User GetUser()
        {
            return new User()
            {
                Name = Name,
                Password = Password,
                Email = Email
            };
        }
    }
}
