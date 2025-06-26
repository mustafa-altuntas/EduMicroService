using EduMicroService.Shared.Filters;
using EduMicroService.Shared.Extensions;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EduMicroService.Identity.Api.Features.Users.UpdateUser;

public static class UpdateUserEndpointExt
{
    public static RouteGroupBuilder MapUpdateUserEndpointExt(this RouteGroupBuilder group)
    {
        group.MapPut("/", async (UpdateUserCommand command, IMediator mediator) =>
        {
            var result = await mediator.Send(command);
            return result.ToGenericResult();
        })
        .WithName("UpdateUser")
        .MapToApiVersion(1, 0)
        .Produces<Guid>(StatusCodes.Status204NoContent)
        .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
        .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
        .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError)
        .AddEndpointFilter<ValidationFilter<UpdateUserCommand>>()
            ;

        return group;
    }
}


