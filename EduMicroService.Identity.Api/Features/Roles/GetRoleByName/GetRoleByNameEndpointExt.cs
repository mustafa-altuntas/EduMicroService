using EduMicroService.Identity.Api.Dto;
using EduMicroService.Shared.Extensions;
using EduMicroService.Shared.Filters;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EduMicroService.Identity.Api.Features.Roles.GetRoleByName
{
    public static class GetRoleByNameEndpointExt
    {
        public static RouteGroupBuilder MapGetRoleByNameEndpointExt(this RouteGroupBuilder group)
        {
            group.MapGet("/{name}", async ([FromServices] IMediator mediator, string name) =>
            {
                var result = await mediator.Send(new GetRoleByNameQuery(name));
                return result.ToGenericResult();
            })
            .MapToApiVersion(1, 0)
            .Produces<RoleDto>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .AddEndpointFilter<ValidationFilter<GetRoleByNameQuery>>()
            ;


            return group;
        }
    }

}
