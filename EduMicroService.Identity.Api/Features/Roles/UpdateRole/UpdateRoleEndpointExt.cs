using EduMicroService.Shared.Extensions;
using EduMicroService.Shared.Filters;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EduMicroService.Identity.Api.Features.Roles.UpdateRole
{
    public static class UpdateRoleEndpointExt
    {
        public static RouteGroupBuilder MapUpdateRoleEndpointExt(this RouteGroupBuilder group)
        {
            group.MapPut("/", async (UpdateRoleCommand command, IMediator mediator) =>
            {
                var result = await mediator.Send(command);
                return result.ToGenericResult();
            })
            .WithName("UpdateRole")
            .MapToApiVersion(1, 0)
            .Produces<Guid>(StatusCodes.Status204NoContent)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError)
            .AddEndpointFilter<ValidationFilter<UpdateRoleCommand>>()
                ;

            return group;
        }
    }
}
