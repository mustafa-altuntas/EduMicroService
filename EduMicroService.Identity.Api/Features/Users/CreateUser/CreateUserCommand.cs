using EduMicroService.Shared;

namespace EduMicroService.Identity.Api.Features.Users.CreateUser;

public record CreateUserCommand(string Email, string Password) : IRequestByServiceResult<Guid>;









