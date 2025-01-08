using Application.Commands_Queries.Users.Commands;
using Application.DTO;
using AutoMapper;
using Core.Entities;
using Core.IRepositories;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class UserService : BaseService, IUserService
    {

        public UserService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        public async Task<UserResponseCommand> AddUserAsync(UserCommand user)
        {
            var req = await UnitOfWork.Users.AddResponseAsync(Mapper.Map<User>(user));
            return new UserResponseCommand
            {
                Id = req,
                IsSuccess = req > 0
            };
        }

        public async Task<IEnumerable<UserViewModel>> GetAllAsync(bool isDapper = false)
        {
            var lst = Enumerable.Empty<User>();
            if (isDapper)
            {
                lst = await UnitOfWork.Users.GetAllDapperAsync();
            }
            else
            {
                lst = await UnitOfWork.Users.GetAllAsync();
            }
            return Mapper.Map<IEnumerable<UserViewModel>>(lst);



        }
    }
}
