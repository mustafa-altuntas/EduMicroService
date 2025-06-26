using Asp.Versioning.Builder;
using EduMicroService.Identity.Api.Features.Roles.CreateRole;
using EduMicroService.Identity.Api.Features.Roles.DeleteRole;
using EduMicroService.Identity.Api.Features.Roles.GetRoleByName;
using EduMicroService.Identity.Api.Features.Roles.GetRoles;
using EduMicroService.Identity.Api.Features.Roles.GetUsersInRole;
using EduMicroService.Identity.Api.Features.Roles.UpdateRole;

namespace EduMicroService.Identity.Api.Features.Roles
{
    public static class RoleEndpointExt
    {
        public static void AddRolesEndpointExt(this WebApplication app, ApiVersionSet apiVersionSet)
        {
            var roleGroup = app.MapGroup("api/v{version:apiVersion}/roles")
                .WithTags("Roles")
                .WithApiVersionSet(apiVersionSet)
                .CreateRoleEndpointExt()
                .MapGetRolesEndpointExt()
                .MapGetRoleByNameEndpointExt()
                .MapDeleteRoleByNameEndpointExt()
                .MapUpdateRoleEndpointExt()
                .MapGetUsersInRoleEndpointExt()
                .RequireAuthorization("Admin")
                ;
        }
    }
}


//POST      /api/roles 
//GET       /api/roles	
//GET       /api/roles/{roleName}
//PUT       /api/roles/{roleName}
//DELETE    /api/roles/{roleName}
//GET       /api/roles/{roleName}/users


