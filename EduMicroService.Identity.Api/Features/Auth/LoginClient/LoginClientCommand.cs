using EduMicroService.Identity.Api.Dto;
using EduMicroService.Shared;

namespace EduMicroService.Identity.Api.Features.Users.LoginClient;
public record LoginClientCommand(string ClientId, string ClientSecret) : IRequestByServiceResult<AuthResponse>;
