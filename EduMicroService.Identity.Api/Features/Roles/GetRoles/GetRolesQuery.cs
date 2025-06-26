using EduMicroService.Identity.Api.Dto;
using EduMicroService.Shared;

namespace EduMicroService.Identity.Api.Features.Roles.GetRoles;

public record GetRolesQuery() : IRequestByServiceResult<List<RoleDto>>;
