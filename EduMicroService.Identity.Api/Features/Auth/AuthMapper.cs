using AutoMapper;
using EduMicroService.Identity.Api.Features.Users.RegisterUser;
using EduMicroService.Identity.Api.Models;

namespace EduMicroService.Identity.Api.Features.Users
{
    public class AuthMapper : Profile
    {
        public AuthMapper()
        {
            CreateMap<RegisterUserCommand, AppUser>().ReverseMap();
        }
    }
}
