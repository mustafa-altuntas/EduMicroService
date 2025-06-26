using FluentValidation;

namespace EduMicroService.Identity.Api.Features.Users.UpdateUser;

public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("User ID cannot be empty.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email address is required.")
            .EmailAddress().WithMessage("A valid email address is required.");

        RuleFor(x => x.UserName)
            .MaximumLength(50).WithMessage("Username must not exceed 50 characters.")
            .When(x => !string.IsNullOrWhiteSpace(x.UserName));

        RuleFor(x => x.PhoneNumber)
            .Matches(@"^\+?[0-9]{10,15}$")
            .WithMessage("A valid phone number is required.")
            .When(x => !string.IsNullOrWhiteSpace(x.PhoneNumber));

        RuleFor(x => x.Password)
            .MinimumLength(3).WithMessage("Password must be at least 3 characters long.")
            //.Matches(@"[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
            //.Matches(@"[a-z]").WithMessage("Password must contain at least one lowercase letter.")
            //.Matches(@"\d").WithMessage("Password must contain at least one number.")
            .When(x => !string.IsNullOrWhiteSpace(x.Password));

    }
}


