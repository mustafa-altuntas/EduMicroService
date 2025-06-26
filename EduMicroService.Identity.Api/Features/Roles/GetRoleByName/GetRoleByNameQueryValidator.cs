using FluentValidation;

namespace EduMicroService.Identity.Api.Features.Roles.GetRoleByName
{
    public class GetRoleByNameQueryValidator : AbstractValidator<GetRoleByNameQuery>
    {
        public GetRoleByNameQueryValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Role name cannot be empty.");
        }
    }
}
