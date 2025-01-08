using Application.Commands_Queries.Users.Commands;
using Application.Services;
using AutoMapper;
using Core.Entities;
using Core.IRepositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handler.Commands.Users
{
    public class UserHandler : IRequestHandler<UserCommand, UserResponseCommand>
    {
        private readonly IUserService _userService;

        public UserHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<UserResponseCommand> Handle(UserCommand request, CancellationToken cancellationToken)
        {
            return await _userService.AddUserAsync(request);
        }
    }
}
