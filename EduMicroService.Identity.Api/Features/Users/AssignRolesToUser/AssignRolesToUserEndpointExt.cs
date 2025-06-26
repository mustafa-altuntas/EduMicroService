using EduMicroService.Shared.Filters;
using EduMicroService.Shared.Extensions;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EduMicroService.Identity.Api.Features.Users.AssignRolesToUser;

public static class AssignRolesToUserEndpointExt
{
    public static RouteGroupBuilder MapAssignRolesToUserEndpointExt(this RouteGroupBuilder group)
    {
        group.MapPost("/assign-roles", async (
            AssignRolesToUserCommand command,
            IMediator mediator) =>
        {
            var result = await mediator.Send(command);
            return result.ToGenericResult();
        })
        .WithName("AssignRolesToUser")
        .WithSummary("Assign roles to a user")
        .WithDescription("Assigns a new set of roles to a specific user, replacing the existing ones.")
        .MapToApiVersion(1,0)
        .Produces(StatusCodes.Status204NoContent)
        .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
        .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
        .AddEndpointFilter<ValidationFilter<AssignRolesToUserCommand>>()
        ;

        return group;
    }
}

