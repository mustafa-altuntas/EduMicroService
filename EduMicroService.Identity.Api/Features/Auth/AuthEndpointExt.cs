using Asp.Versioning.Builder;
using EduMicroService.Identity.Api.Features.Auth.RefreshToken;
using EduMicroService.Identity.Api.Features.Auth.RevokeRefreshToken;
using EduMicroService.Identity.Api.Features.Users.LoginClient;
using EduMicroService.Identity.Api.Features.Users.LoginUser;
using EduMicroService.Identity.Api.Features.Users.RegisterUser;

namespace EduMicroService.Identity.Api.Features.Users
{
    public static class AuthEndpointExt
    {
        public static void AddAuthsEndpointExt(this WebApplication app, ApiVersionSet apiVersionSet)
        {
            var authGroup = app.MapGroup("api/v{version:apiVersion}/auth")
                .WithTags("Auth")
                .WithApiVersionSet(apiVersionSet)
                .RegisterUserEndpointExt()
                .LoginUserEndpointExt()
                .LoginClientEndpointExt()
                .RefreshTokenEndpointExt()   // refresh-token
                .RevokeRefreshTokenExt().RequireAuthorization()    // revoke-token RevokeRefreshToken
                ;
            
                authGroup.MapGet("/p1", ()
                =>
                {
                    return Results.Ok("p1 client authorization test edildi.");
                })
                .WithName("p1")
                .MapToApiVersion(1, 0)
                .RequireAuthorization()
                ;

                authGroup.MapGet("/p2", ()
                =>
                {
                    return Results.Ok("p2 test edildi.");
                })
                .WithName("p2")
                .MapToApiVersion(1, 0)
                .RequireAuthorization()
                ;

 



        }
    }
}
