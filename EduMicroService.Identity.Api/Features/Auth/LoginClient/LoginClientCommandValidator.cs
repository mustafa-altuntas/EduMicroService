using FluentValidation;

namespace EduMicroService.Identity.Api.Features.Users.LoginClient
{
    public class LoginClientCommandValidator : AbstractValidator<LoginClientCommand>
    {
        public LoginClientCommandValidator()
        {
            RuleFor(x => x.ClientId)
            .NotEmpty().WithMessage("ClientId boş olamaz.");

            RuleFor(x => x.ClientSecret)
                .NotEmpty().WithMessage("Client secret boş olamaz.");
        }
    }

}
