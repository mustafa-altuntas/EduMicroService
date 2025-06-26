using EduMicroService.Shared;

namespace EduMicroService.Identity.Api.Features.Users.AssignRolesToUser;

public sealed record AssignRolesToUserCommand(Guid UserId, List<string> Roles) : IRequestByServiceResult;

 