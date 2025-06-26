using EduMicroService.Shared;

namespace EduMicroService.Identity.Api.Features.Roles.DeleteRole;

public record DeleteRoleCommand(string Name) : IRequestByServiceResult;

