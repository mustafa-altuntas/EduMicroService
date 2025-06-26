using EduMicroService.Identity.Api.Dto;
using EduMicroService.Shared.Extensions;
using EduMicroService.Shared.Filters;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EduMicroService.Identity.Api.Features.Users.LoginUser
{
    public static class LoginUserEndpoint
    {
        public static RouteGroupBuilder LoginUserEndpointExt(this RouteGroupBuilder group)
        {
            group.MapPost("/login", async (LoginUserCommand command, IMediator mediator)
                => (await mediator.Send(command)).ToGenericResult())
            .WithName("LoginUser")
            .MapToApiVersion(1, 0)
            .Produces<AuthResponse>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError)
            //.AddEndpointFilter<ValidationFilter<LoginUserCommand>>()
            .AllowAnonymous()
            ;

            return group;
        }
    }
}
