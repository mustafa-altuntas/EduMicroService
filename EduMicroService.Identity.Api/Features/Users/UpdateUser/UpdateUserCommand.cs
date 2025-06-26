using EduMicroService.Shared;

namespace EduMicroService.Identity.Api.Features.Users.UpdateUser;
public record UpdateUserCommand(string Id, string Email, string? UserName, string? PhoneNumber, string? Password) : IRequestByServiceResult;


