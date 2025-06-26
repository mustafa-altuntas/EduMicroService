using FluentValidation;

namespace EduMicroService.Identity.Api.Features.Roles.GetUsersInRole
{
    public class GetUsersInRoleQueryValidator : AbstractValidator<GetUsersInRoleQuery>
    {
        public GetUsersInRoleQueryValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Role name is required.");
        }
    }
}
