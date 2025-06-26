using EduMicroService.Shared;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace EduMicroService.Identity.Api.Features.Roles.DeleteRole
{
    public class DeleteRoleCommandHandler(RoleManager<AppRole> roleManager) : IRequestHandler<DeleteRoleCommand, ServiceResult>
    {
        public async Task<ServiceResult> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            var role = await roleManager.FindByNameAsync(request.Name);

            if (role == null)
            {
                return ServiceResult.ErrorAsNotFound();
            }

            var result = await roleManager.DeleteAsync(role);
            if (!result.Succeeded)
            {
                return ServiceResult.Error("Role deletion failed", result);
            }

            return ServiceResult.SuccessAsNoContent();

        }
    }
}
