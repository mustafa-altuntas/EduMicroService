using EduMicroService.Identity.Api.Dto;
using EduMicroService.Shared.Extensions;
using EduMicroService.Shared.Filters;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EduMicroService.Identity.Api.Features.Roles.GetUsersInRole
{
    public static class GetUsersInRoleEndpointExt
    {
        public static RouteGroupBuilder MapGetUsersInRoleEndpointExt(this RouteGroupBuilder group)
        {
            group.MapGet("/{name}/users", async (string name, [FromServices]IMediator mediator) =>
            {
                var result = await mediator.Send(new GetUsersInRoleQuery(name));
                return result.ToGenericResult();
            })
            .WithName("GetUsersInRole")
            .MapToApiVersion(1, 0)
            .Produces<List<UserDto>>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError)
            .AddEndpointFilter<ValidationFilter<GetUsersInRoleQuery>>()
                ;

            return group;
        }
    }
}
