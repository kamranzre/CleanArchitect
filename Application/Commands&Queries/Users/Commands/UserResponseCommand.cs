using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands_Queries.Users.Commands
{
    public class UserResponseCommand
    {
        public int Id { get; set; }

        public bool IsSuccess { get; set; }
    }
}
