using EduMicroService.Shared;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Net;

namespace EduMicroService.Identity.Api.Features.Roles.CreateRole
{
    public class CreateRoleCommandHandler(RoleManager<AppRole> roleManager) : IRequestHandler<CreateRoleCommand, ServiceResult<string>>
    {

        public async Task<ServiceResult<string>> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {

            var roleExists = await roleManager.RoleExistsAsync(request.RoleName);
            if (roleExists)
                return ServiceResult<string>.Error("Role already exists.", "Role already exists.", HttpStatusCode.Conflict);

            var result = await roleManager.CreateAsync(new AppRole { Name = request.RoleName });
            if (!result.Succeeded)
                return ServiceResult<string>.Error("Identity operation failed", result);

            return ServiceResult<string>.SuccessAsCreated(request.RoleName, $"/api/roles/{request.RoleName}");
        }
    }

}



