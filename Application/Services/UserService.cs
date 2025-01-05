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
