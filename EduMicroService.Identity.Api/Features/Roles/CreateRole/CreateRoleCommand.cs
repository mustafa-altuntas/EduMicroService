using EduMicroService.Shared;

namespace EduMicroService.Identity.Api.Features.Roles.CreateRole;
public record CreateRoleCommand(string RoleName) : IRequestByServiceResult<string>;




