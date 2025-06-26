using EduMicroService.Identity.Api.Dto;
using EduMicroService.Shared;

namespace EduMicroService.Identity.Api.Features.Users.LoginUser;
public record LoginUserCommand(string Email, string Password) : IRequestByServiceResult<AuthResponse>;
