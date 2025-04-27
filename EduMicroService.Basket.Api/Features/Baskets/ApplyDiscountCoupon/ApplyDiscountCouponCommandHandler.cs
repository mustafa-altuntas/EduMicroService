using EduMicroService.Basket.Api.Const;
using EduMicroService.Shared;
using EduMicroService.Shared.Services;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using System.Net;
using System.Text.Json;

namespace EduMicroService.Basket.Api.Features.Baskets.ApplyDiscountCoupon
{
    public class ApplyDiscountCouponCommandHandler(IIdentityService identityService, IDistributedCache distributedCache) : IRequestHandler<ApplyDiscountCouponCommand, ServiceResult>
    {
        public async Task<ServiceResult> Handle(ApplyDiscountCouponCommand request, CancellationToken cancellationToken)
        {
            var cacheKey = string.Format(BasketConst.BasketCacheKey, identityService.GetUserId);
            var basketAsJson = await distributedCache.GetStringAsync(cacheKey, token: cancellationToken);

            if (string.IsNullOrEmpty(basketAsJson))
            {
                return ServiceResult.Error("Basket not found", HttpStatusCode.NotFound);
            }

            var basket = JsonSerializer.Deserialize<Data.Basket>(basketAsJson)!;

            if (!basket.BasketItems.Any())
            {
                return ServiceResult.Error("Basket item not found", HttpStatusCode.NotFound);
            }



            basket.ApplyNewDiscount(request.Coupon,request.DiscountRate);

            basketAsJson = JsonSerializer.Serialize(basket);
            await distributedCache.SetStringAsync(cacheKey, basketAsJson, token: cancellationToken);

            return ServiceResult.SuccessAsNoContent();


        }
    }
}
