using EduMicroService.Shared;
using EduMicroService.Shared.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace EduMicroService.Basket.Api.Features.Baskets.RemoveDiscountCoupon
{
    public record RemoveDiscountCouponCommand : IRequestByServiceResult;

    public class RemoveDiscountCouponHandler(BasketService basketService) : IRequestHandler<RemoveDiscountCouponCommand, ServiceResult>
    {
        public async Task<ServiceResult> Handle(RemoveDiscountCouponCommand request, CancellationToken cancellationToken)
        {
            var basketAsString = await basketService.GetBasketFromCacheAsync(cancellationToken);

            if (string.IsNullOrEmpty(basketAsString))
            {
                return ServiceResult.Error("Basket not found", HttpStatusCode.NotFound);
            }

            var basket = JsonSerializer.Deserialize<Data.Basket>(basketAsString)!;

            basket.ClearDiscountRate();

            await basketService.CreateBasketToCacheAsync(basket, cancellationToken);
            return ServiceResult.SuccessAsNoContent();
        }
    }

    public static class RemoveDiscountCouponEndpoint
    {
        public static RouteGroupBuilder RemoveDiscountCouponEndpointExt(this RouteGroupBuilder group)
        {
            group.MapDelete("/remove-discount-coupon",
                    async ([FromServices] IMediator mediator) =>
                        (await mediator.Send(new RemoveDiscountCouponCommand())).ToGenericResult())
                .WithName("RemoveDiscountCoupon")
                .MapToApiVersion(1, 0);

            return group;
        }
    }



}
