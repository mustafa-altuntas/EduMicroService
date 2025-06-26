using FluentValidation;

namespace EduMicroService.Identity.Api.Features.Users.CreateUser
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Email is required.");

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Password is required.");

        }
    }


}




