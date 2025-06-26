using EduMicroService.Identity.Api.Dto;
using EduMicroService.Shared;

namespace EduMicroService.Identity.Api.Features.Auth.RefreshToken;
public record RefreshTokenCommand(string RefreshToken) : IRequestByServiceResult<AuthResponse>;
