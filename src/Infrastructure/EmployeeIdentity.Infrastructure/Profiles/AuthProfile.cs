using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EmployeeIdentity.Application.DTOs.Auth;
using EmployeeIdentity.Persistence.IdentityModels;

namespace EmployeeIdentity.Infrastructure.Profiles
{
    public class AuthProfile : Profile
    {
        public AuthProfile()
        {
            CreateMap<GetUserDTO, ApplicationUser>().ReverseMap();
        }
    }
}