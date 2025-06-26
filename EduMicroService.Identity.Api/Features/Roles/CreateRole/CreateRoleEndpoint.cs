using EduMicroService.Shared.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EduMicroService.Identity.Api.Features.Roles.CreateRole
{
    public static class CreateRoleEndpoint
    {
        public static RouteGroupBuilder CreateRoleEndpointExt(this RouteGroupBuilder group)
        {
            group.MapPost("/", async (CreateRoleCommand command, IMediator mediator) =>
            {
                var result = await mediator.Send(command);
                return result.ToGenericResult();
            })
            .WithName("CreateRole")
            .MapToApiVersion(1, 0)
            .WithSummary("Creates a new role")
            .WithDescription("Creates a new identity role using MediatR and RoleManager")
            .Produces<Guid>(StatusCodes.Status201Created)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError)
            //.AddEndpointFilter<ValidationFilter<CreateRoleCommand>>()

            ;

            return group;
        }
    }

}



