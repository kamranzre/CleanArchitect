using Application.Commands_Queries.Users.Commands;
using Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface IUserService
    {
        Task<IEnumerable<UserViewModel>> GetAllAsync(bool isDapper = false);

        Task<UserResponseCommand> AddUserAsync(UserCommand user);
    }
}
