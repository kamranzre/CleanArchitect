using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO
{
    public class UserViewModel:BaseEntityModel
    {
        public UserViewModel()
        {        
        }

        public required string FullName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string UserName { get; set; } = string.Empty;

        public string NationalCode { get; set; }

    }
}
