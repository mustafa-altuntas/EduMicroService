using EduMicroService.Shared;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Net;

namespace EduMicroService.Identity.Api.Features.Roles.UpdateRole
{
    public class UpdateRoleCommandHandler(RoleManager<AppRole> roleManager) : IRequestHandler<UpdateRoleCommand, ServiceResult>
    {
        public async Task<ServiceResult> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
        {

            var roleExists = await roleManager.FindByNameAsync(request.Name);
            if (roleExists is not null)
                return ServiceResult.Error("Role already exists.", "Role already exists.", HttpStatusCode.Conflict);

            var role = await roleManager.FindByIdAsync(request.Id);
            if (role is null)
                return ServiceResult.ErrorAsNotFound();

            role.Name = request.Name;
            var result = await roleManager.UpdateAsync(role);
            if (!result.Succeeded)
                return ServiceResult.Error("Identity operation failed", result);

            return ServiceResult.SuccessAsNoContent();

        }
    }
}
