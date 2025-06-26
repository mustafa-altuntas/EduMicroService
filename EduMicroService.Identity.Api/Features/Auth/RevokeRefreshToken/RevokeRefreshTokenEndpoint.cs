using EduMicroService.Shared.Extensions;
using EduMicroService.Shared.Filters;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EduMicroService.Identity.Api.Features.Auth.RevokeRefreshToken
{
    public static class RevokeRefreshTokenEndpoint
    {
        public static RouteGroupBuilder RevokeRefreshTokenExt(this RouteGroupBuilder group)
        {
            group.MapPost("/revoke-refresh-token", async (RevokeRefreshTokenCommand command, IMediator mediator)
                => (await mediator.Send(command)).ToGenericResult())
                .WithName("RevokeRefreshToken")
                .MapToApiVersion(1, 0)
                .Produces(StatusCodes.Status204NoContent)
                .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
                .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
                .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError)
                .AddEndpointFilter<ValidationFilter<RevokeRefreshTokenValidator>>()
                
                ;


            return group;
        }
    }
}
