using AutoMapper;
using EduMicroService.Identity.Api.Dto;
using EduMicroService.Shared;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace EduMicroService.Identity.Api.Features.Roles.GetRoleByName;

public class GetRoleByNameQueryHandler(RoleManager<AppRole> _roleManager, IMapper mapper) : IRequestHandler<GetRoleByNameQuery, ServiceResult<RoleDto>>
{
    public async Task<ServiceResult<RoleDto>> Handle(GetRoleByNameQuery request, CancellationToken cancellationToken)
    {
        var roles = await _roleManager.FindByNameAsync(request.Name);
        if (roles == null)
        {
            return ServiceResult<RoleDto>.ErrorAsNotFound();
        }
        return ServiceResult<RoleDto>.SuccessAsOk(mapper.Map<RoleDto>(roles));
    }
}