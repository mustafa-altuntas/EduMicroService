using EduMicroService.Identity.Api.Dto;
using EduMicroService.Shared.Extensions;
using EduMicroService.Shared.Filters;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EduMicroService.Identity.Api.Features.Users.LoginClient
{
    public static class LoginClientEndpoint
    {
        public static RouteGroupBuilder LoginClientEndpointExt(this RouteGroupBuilder group)
        {
            group.MapPost("/loginbyclient", async (LoginClientCommand command, IMediator mediator)
                => (await mediator.Send(command)).ToGenericResult())
            .WithName("LoginClient")
            .MapToApiVersion(1, 0)
            .Produces<AuthResponse>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError)
            .AddEndpointFilter<ValidationFilter<LoginClientCommand>>()
            .AllowAnonymous()
            ;

            return group;
        }
    }
}
