using EduMicroService.Identity.Api.Dto;
using EduMicroService.Shared.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EduMicroService.Identity.Api.Features.Roles.GetRoles
{
    public static class GetRolesEndpointExt
    {
        public static RouteGroupBuilder MapGetRolesEndpointExt(this RouteGroupBuilder group)
        {
            group.MapGet("/", async ([FromServices]IMediator mediator) =>
            {
                var result = await mediator.Send(new GetRolesQuery());
                return result.ToGenericResult();
            })
            .WithName("GetRoles")
            .WithSummary("List all roles")
            .WithDescription("Returns all defined roles in the system.")
            .MapToApiVersion(1, 0)
            .Produces<List<RoleDto>>(StatusCodes.Status200OK);

            return group;
        }
    }

}
