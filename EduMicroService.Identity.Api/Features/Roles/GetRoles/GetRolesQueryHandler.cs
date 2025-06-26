using AutoMapper;
using EduMicroService.Identity.Api.Dto;
using EduMicroService.Shared;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EduMicroService.Identity.Api.Features.Roles.GetRoles;

public class GetRolesQueryHandler(RoleManager<AppRole> _roleManager, IMapper mapper) : IRequestHandler<GetRolesQuery, ServiceResult<List<RoleDto>>>
{
    public async Task<ServiceResult<List<RoleDto>>> Handle(GetRolesQuery request, CancellationToken cancellationToken)
    {
        var roles = await _roleManager.Roles.ToListAsync(cancellationToken);
        if (roles == null || roles.Count == 0)
        {
            roles = new ();
        }
        return ServiceResult<List<RoleDto>>.SuccessAsOk(mapper.Map<List<RoleDto>>(roles));
    }
}