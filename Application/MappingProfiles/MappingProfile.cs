using Application.Commands_Queries.Users.Commands;
using Application.DTO;
using AutoMapper;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MappingProfiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserViewModel>()
                .ForMember(x => x.FullName, y => y.MapFrom(z => z.Name))
                .ReverseMap();

            CreateMap<User, UserCommand>()
                .ReverseMap();
        }

    }
}
