using AutoMapper;
using EduMicroService.Identity.Api.Dto;
using EduMicroService.Identity.Api.Models;
using EduMicroService.Shared;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace EduMicroService.Identity.Api.Features.Roles.GetUsersInRole
{
    public class GetUsersInRoleQueryHandler(RoleManager<AppRole> roleManager, UserManager<AppUser> userManager, IMapper mapper) : IRequestHandler<GetUsersInRoleQuery, ServiceResult<List<UserDto>>>
    {
        public async Task<ServiceResult<List<UserDto>>> Handle(GetUsersInRoleQuery request, CancellationToken cancellationToken)
        {
            var usersInRole = new List<AppUser>();

            var role = await roleManager.FindByNameAsync(request.Name);
            if (role == null)
            {
                return ServiceResult<List<UserDto>>.ErrorAsNotFound();
            }

            var users = (await userManager.GetUsersInRoleAsync(request.Name));

            var userDtos = mapper.Map<List<UserDto>>(users);

            return ServiceResult<List<UserDto>>.SuccessAsOk(userDtos);
        }
    }
}
