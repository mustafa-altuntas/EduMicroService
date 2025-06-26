using EduMicroService.Identity.Api.Dto;
using EduMicroService.Shared.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EduMicroService.Identity.Api.Features.Auth.RefreshToken
{
    public static class RefreshTokenEndpoint
    {
        public static RouteGroupBuilder RefreshTokenEndpointExt(this RouteGroupBuilder group)
        {
            group.MapPost("/refresh-token", async (RefreshTokenCommand command, IMediator mediator)
                => (await mediator.Send(command)).ToGenericResult())
            .WithName("RefreshToken")
            .MapToApiVersion(1, 0)
            .Produces<AuthResponse>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError)
            //.AddEndpointFilter<ValidationFilter<RefreshTokenCommand>>()
            ;

            return group;
        }
    }
}
