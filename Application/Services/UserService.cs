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

        public async Task InsertRandomUsersAsync(int count)
        {
            var userMap = GenerateRandomUsers(count).Select(x => new User()
            {
                Name = x.Name,
                NationalCode = x.NationalCode,
                UserName = x.Name,
                CreateDate = DateTime.Now,
                EditationDate = DateTime.Now,
                UserCreate = x.UserCreate,
                Id = x.Id,
                Email = x.Email,
                Password = x.Password,
                UserEditation = x.UserEditation,
            }).ToList();
         
          
            var query = @"
            INSERT INTO Users (Name, Email, Password, UserName, NationalCode, CreateDate, EditationDate, UserCreate, UserEditation)
            VALUES (@Name, @Email, @Password, @UserName, @NationalCode, @CreateDate, @EditationDate, @UserCreate, @UserEditation)";
         await  UnitOfWork.Users.InsertListDapperAsync(query, userMap.ToList());
        }

        private List<UserDTO> GenerateRandomUsers(int count)
        {
            var users = new List<UserDTO>();
            var random = new Random();

            for (int i = 0; i < count; i++)
            {
                var user = new UserDTO
                {
                    Name = $"User{i + 1}",
                    Email = $"user{i + 1}@example.com",
                    Password = Guid.NewGuid().ToString(), // رمز عبور رندوم
                    UserName = $"user{i + 1}",
                    NationalCode = "2281999629", // کد ملی رندوم
                    CreateDate = DateTime.Now,
                    EditationDate = DateTime.Now, // یا null برای برخی رکوردها
                    UserCreate = random.Next(1, 1000), // کاربر ایجاد کننده رندوم
                    UserEditation = random.Next(1, 1000) // کاربر ویرایش کننده رندوم
                };
                users.Add(user);
            }

            return users;
        }
    }
}
