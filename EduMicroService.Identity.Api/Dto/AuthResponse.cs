namespace EduMicroService.Identity.Api.Dto;
public record AuthResponse(string AccessToken, string RefreshToken, DateTime ExpiresAt);

