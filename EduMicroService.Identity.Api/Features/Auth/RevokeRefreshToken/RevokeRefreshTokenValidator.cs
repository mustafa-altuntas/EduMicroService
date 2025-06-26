using FluentValidation;

namespace EduMicroService.Identity.Api.Features.Auth.RevokeRefreshToken
{
    public class RevokeRefreshTokenValidator : AbstractValidator<RevokeRefreshTokenCommand>
    {
        public RevokeRefreshTokenValidator()
        {
            RuleFor(x => x.RefreshToken)
                .NotEmpty()
                .WithMessage("Refresh token boş olamaz.");
        }
    }
}
