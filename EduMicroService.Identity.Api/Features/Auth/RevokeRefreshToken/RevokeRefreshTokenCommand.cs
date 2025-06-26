using EduMicroService.Shared;

namespace EduMicroService.Identity.Api.Features.Auth.RevokeRefreshToken;
public record RevokeRefreshTokenCommand(string RefreshToken) : IRequestByServiceResult;
