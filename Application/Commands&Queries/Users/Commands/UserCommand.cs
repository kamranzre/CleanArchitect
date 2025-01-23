using Application.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands_Queries.Users.Commands
{
    public class UserCommand : BaseEntityModel<long>, IRequest<UserResponseCommand>
    {
        public required string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string UserName { get; set; } = string.Empty;

        public required string NationalCode { get; set; }

    }
}
