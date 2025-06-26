using Asp.Versioning.Builder;
using EduMicroService.Identity.Api.Features.Users.AssignRolesToUser;
using EduMicroService.Identity.Api.Features.Users.CreateUser;
using EduMicroService.Identity.Api.Features.Users.DeleteUser;
using EduMicroService.Identity.Api.Features.Users.GetUserById;
using EduMicroService.Identity.Api.Features.Users.GetUserRoles;
using EduMicroService.Identity.Api.Features.Users.GetUsers;
using EduMicroService.Identity.Api.Features.Users.UpdateUser;

namespace EduMicroService.Identity.Api.Features.Users
{
    public static class UserEndpointExt
    {
        public static RouteGroupBuilder AddUsersEndpointExt(this WebApplication app, ApiVersionSet apiVersionSet)
        {
            var group = app.MapGroup("api/v{version:apiVersion}/users")
                .WithTags("Users")
                .WithApiVersionSet(apiVersionSet)
                .MapCreateUserEndpointExt()
                .MapGetUsersEndpointExt()
                .MapGetUserByIdEndpointExt()
                .MapUpdateUserEndpointExt()
                .MapDeleteUserEndpointExt()
                .MapAssignRolesToUserEndpointExt()
                .MapGetUserRolesEndpointExt()
                //.MapRemoveUserRoleEndpointExt() // assign role ile aynı işlem buna gerek yok
                //.RequireAuthorization("Admin")
                ;

            return group;
        }
    }
}


//POST          /api/users                      -> Yeni kullanıcı oluşturur
//GET           /api/users                      -> Tüm kullanıcıları listeler
//GET           /api/users/{id}                 -> Belirli bir kullanıcıyı getirir
//PUT           /api/users/{id}                 -> Kullanıcı bilgilerini günceller
//DELETE        /api/users/{id}                 -> Kullanıcıyı siler
//POST          /api/users/{id}/assign-roles           -> Kullanıcıya bir veya daha fazla rol atar
//GET           /api/users/{id}/roles           -> Kullanıcının rollerini listeler
//DELETE        /api/users/{id}/roles/{ role}   -> Kullanıcıdan belirli bir rolü kaldırır