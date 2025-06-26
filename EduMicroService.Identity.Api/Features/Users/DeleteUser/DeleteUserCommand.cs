using EduMicroService.Shared;

namespace EduMicroService.Identity.Api.Features.Users.DeleteUser;

public sealed record DeleteUserCommand(Guid Id) : IRequestByServiceResult<Guid>;
