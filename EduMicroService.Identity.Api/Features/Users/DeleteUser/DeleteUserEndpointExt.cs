using EduMicroService.Shared.Extensions;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EduMicroService.Identity.Api.Features.Users.DeleteUser;

public static class DeleteUserEndpointExt
{
    public static RouteGroupBuilder MapDeleteUserEndpointExt(this RouteGroupBuilder group)
    {
        group.MapDelete("/{id:guid}", async (Guid id, ISender sender) =>
        {
            var result = await sender.Send(new DeleteUserCommand(id));
            return result.ToGenericResult();
        })
                .WithName("DeleteUser")
                .WithSummary("Deletes a user by ID")
                .WithDescription("Deletes the user that matches the provided ID if found in the system")
                .MapToApiVersion(1, 0)
                .Produces<Guid>(StatusCodes.Status200OK)
                .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
                .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
                ;

        return group;

    }
}
 