using AutoMapper;
using EduMicroService.Identity.Api.Dto;

namespace EduMicroService.Identity.Api.Features.Roles
{
    public class RolesMapper : Profile
    {
        public RolesMapper()
        {
            CreateMap<AppRole, RoleDto>().ReverseMap();
        }
    }
}
