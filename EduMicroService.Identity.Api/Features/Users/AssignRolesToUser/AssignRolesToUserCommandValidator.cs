using FluentValidation;

namespace EduMicroService.Identity.Api.Features.Users.AssignRolesToUser;

public class AssignRolesToUserCommandValidator : AbstractValidator<AssignRolesToUserCommand>
{
    public AssignRolesToUserCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("User id is required.");
        RuleFor(x => x.Roles)
            .NotNull().WithMessage("The roles list cannot be null.");

    }
}
