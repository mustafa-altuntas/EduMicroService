using EduMicroService.Shared.Filters;
using EduMicroService.Shared.Extensions;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EduMicroService.Identity.Api.Features.Users.CreateUser
{
    public static class CreateUserEndpointExt
    {
        public static RouteGroupBuilder MapCreateUserEndpointExt(this RouteGroupBuilder group)
        {
            group.MapPost("/", async (CreateUserCommand command, IMediator mediator) =>
            {
                var result = await mediator.Send(command);
                return result.ToGenericResult();
            })
            .WithName("CreateUser")
            .MapToApiVersion(1, 0)
            .Produces<Guid>(StatusCodes.Status201Created)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError)
            .AddEndpointFilter<ValidationFilter<CreateUserCommand>>()
                ;

            return group;
        }
    }





}




