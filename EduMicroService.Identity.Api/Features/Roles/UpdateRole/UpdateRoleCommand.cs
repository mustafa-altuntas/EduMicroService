using EduMicroService.Shared;

namespace EduMicroService.Identity.Api.Features.Roles.UpdateRole;

public record UpdateRoleCommand(string Id, string Name):IRequestByServiceResult;

