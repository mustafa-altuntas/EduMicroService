using MediatR;
using Microsoft.AspNetCore.Mvc;
using EduMicroService.Shared.Extensions;
using EduMicroService.Shared.Filters;

namespace EduMicroService.Identity.Api.Features.Roles.DeleteRole
{
    public static class DeleteRoleEndpointExt
    {
        public static RouteGroupBuilder MapDeleteRoleByNameEndpointExt(this RouteGroupBuilder group)
        {
            group.MapDelete("/", async ([FromServices] IMediator mediator, [FromBody] DeleteRoleCommand command) =>
            {
                var result = await mediator.Send(command);
                return result.ToGenericResult();
            })
            .WithName("DeleteRole")
            .MapToApiVersion(1, 0)
            .Produces(StatusCodes.Status204NoContent)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .AddEndpointFilter<ValidationFilter<DeleteRoleCommand>>()
            ;
            return group;
        }
    }
}



