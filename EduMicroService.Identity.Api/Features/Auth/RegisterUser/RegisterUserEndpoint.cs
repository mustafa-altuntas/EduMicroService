using EduMicroService.Shared.Extensions;
using EduMicroService.Shared.Filters;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EduMicroService.Identity.Api.Features.Users.RegisterUser
{
    public static class RegisterUserEndpoint
    {
        public static RouteGroupBuilder RegisterUserEndpointExt(this RouteGroupBuilder group)
        {
            group.MapPost("/register", async (RegisterUserCommand command, IMediator mediator)
                => (await mediator.Send(command)).ToGenericResult())
            .WithName("RegisterUser")
            .MapToApiVersion(1, 0)
            .Produces<Guid>(StatusCodes.Status201Created)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError)
            .AddEndpointFilter<ValidationFilter<RegisterUserCommand>>();

            return group;
        }
    }
}
