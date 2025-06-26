using EduMicroService.Identity.Api.Dto;
using EduMicroService.Shared;

namespace EduMicroService.Identity.Api.Features.Roles.GetRoleByName;

public record GetRoleByNameQuery(string Name) : IRequestByServiceResult<RoleDto>;
