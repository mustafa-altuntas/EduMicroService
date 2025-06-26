using AutoMapper;
using EduMicroService.Identity.Api.Dto;
using EduMicroService.Identity.Api.Features.Users.CreateUser;
using EduMicroService.Identity.Api.Models;

namespace EduMicroService.Identity.Api.Features.Users
{
    public class UserMapper : Profile
    {
        public UserMapper()
        {
            CreateMap<AppUser, UserDto>().ReverseMap();
            CreateMap<AppUser, CreateUserCommand>().ReverseMap();
        }
    }
}
