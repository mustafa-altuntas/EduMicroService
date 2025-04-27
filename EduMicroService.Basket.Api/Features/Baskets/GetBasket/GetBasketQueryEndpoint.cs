using EduMicroService.Basket.Api.Dto;
using EduMicroService.Shared.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EduMicroService.Basket.Api.Features.Baskets.GetBasket
{
    public static class GetBasketQueryEndpoint
    {
        public static RouteGroupBuilder GetBasketEndpointExt(this RouteGroupBuilder group)
        {
            group.MapGet("/user", async ([FromServices] IMediator mediator)
                => (await mediator.Send(new GetBasketQuery())).ToGenericResult())
                .WithName("GetBasket")
                .MapToApiVersion(1, 0)
                .Produces<BasketDto>(StatusCodes.Status200OK);


            return group;
        }
    }
}
