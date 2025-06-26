using EduMicroService.Shared.Extensions;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EduMicroService.Identity.Api.Features.Users.GetUserRoles;

public static class GetUserRolesEndpointExt
{
    public static RouteGroupBuilder MapGetUserRolesEndpointExt(this RouteGroupBuilder group)
    {
        group.MapGet("/{userId:guid}/roles", async (
            Guid userId,
            IMediator mediator) =>
        {
            var query = new GetUserRolesQuery(userId);
            var result = await mediator.Send(query);
            return result.ToGenericResult();
        })
        .WithName("GetUserRoles")
        .WithSummary("Get roles of a user")
        .WithDescription("Returns the roles assigned to a specific user.")
        .MapToApiVersion(1,0)
        .Produces<List<string>>(StatusCodes.Status200OK)
        .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
        .Produces<ProblemDetails>(StatusCodes.Status404NotFound);

        return group;
    }
}
