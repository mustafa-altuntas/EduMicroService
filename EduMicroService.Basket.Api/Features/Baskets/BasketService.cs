using EduMicroService.Basket.Api.Const;
using EduMicroService.Shared.Services;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace EduMicroService.Basket.Api.Features.Baskets
{
    public class BasketService(IIdentityService identityService, IDistributedCache distributedCache)
    {

        private string GetCacheKey() => string.Format(BasketConst.BasketCacheKey, identityService.GetUserId);

        public  Task<string?> GetBasketFromCacheAsync(CancellationToken cancellationToken)
        {
            return   distributedCache.GetStringAsync(GetCacheKey(), token: cancellationToken);
        }

        public async Task CreateBasketToCacheAsync(Data.Basket basket, CancellationToken cancellationToken)
        {
            string basketAsString = JsonSerializer.Serialize(basket);
            await distributedCache.SetStringAsync(GetCacheKey(), basketAsString, token: cancellationToken);
        }

    }
}
