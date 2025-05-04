using EduMicroService.Order.Application.Features.Orders.GetOrdersByUserId;
using EduMicroService.Shared.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EduMicroService.Order.Api.Endpoints.Orders
{
    public static class GetOrdersByUserIdEndpoint
    {
        public static RouteGroupBuilder GetOrdersByUserIdEndpointExt(this RouteGroupBuilder group)
        {
            group.MapGet("/", async ([FromServices] IMediator mediator)
                => (await mediator.Send(new GetOrdersByUserIdQuery())).ToGenericResult())
            .WithName("GetOrdersByUserId")
            .MapToApiVersion(1, 0)
            .Produces<Guid>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError)
            ;

            return group;
        }
    }
}
