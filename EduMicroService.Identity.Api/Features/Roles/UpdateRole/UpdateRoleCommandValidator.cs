using FluentValidation;

namespace EduMicroService.Identity.Api.Features.Roles.UpdateRole
{
    public class UpdateRoleCommandValidator : AbstractValidator<UpdateRoleCommand>
    {
        public UpdateRoleCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Role id is required.");
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Role name is required.");
            
        }
    }
}
