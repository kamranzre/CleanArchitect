using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class User:BaseEntity
    {
        public User()
        {

        }
        public required string  Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string UserName { get; set; } = string.Empty;

        public string NationalCode { get; set; }
    }
}
