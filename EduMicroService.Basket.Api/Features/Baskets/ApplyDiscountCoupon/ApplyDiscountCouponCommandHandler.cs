using EduMicroService.Shared;
using MediatR;
using System.Net;
using System.Text.Json;

namespace EduMicroService.Basket.Api.Features.Baskets.ApplyDiscountCoupon
{
    public class ApplyDiscountCouponCommandHandler(BasketService basketService) : IRequestHandler<ApplyDiscountCouponCommand, ServiceResult>
    {
        public async Task<ServiceResult> Handle(ApplyDiscountCouponCommand request, CancellationToken cancellationToken)
        {
            var basketAsJson = await basketService.GetBasketFromCacheAsync(cancellationToken);

            if (string.IsNullOrEmpty(basketAsJson))
            {
                return ServiceResult.Error("Basket not found", HttpStatusCode.NotFound);
            }

            var basket = JsonSerializer.Deserialize<Data.Basket>(basketAsJson)!;

            if (!basket.BasketItems.Any())
            {
                return ServiceResult.Error("Basket item not found", HttpStatusCode.NotFound);
            }



            basket.ApplyNewDiscount(request.Coupon, request.DiscountRate);


            await basketService.CreateBasketToCacheAsync(basket, cancellationToken);


            return ServiceResult.SuccessAsNoContent();


        }
    }
}
