using EduMicroService.Basket.Api.Const;
using EduMicroService.Basket.Api.Data;
using EduMicroService.Shared;
using EduMicroService.Shared.Services;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace EduMicroService.Basket.Api.Features.Baskets.AddBasketItem
{
    public class AddBasketItemCommandHandler(IDistributedCache distributedCache, IIdentityService identityService)
         : IRequestHandler<AddBasketItemCommand, ServiceResult>
    {
        public async Task<ServiceResult> Handle(AddBasketItemCommand request, CancellationToken cancellationToken)
        {
            Guid userId = identityService.GetUserId;
            var cacheKey = string.Format(BasketConst.BasketCacheKey, userId);
            var basketAsString = await distributedCache.GetStringAsync(cacheKey, token: cancellationToken);


            Data.Basket? currentBasket;

            var newBasketItem = new BasketItem(request.CourseId, request.CourseName, request.ImageUrl,
                request.CoursePrice, null);


            if (string.IsNullOrEmpty(basketAsString))
            {
                currentBasket = new Data.Basket(userId, [newBasketItem]);
                basketAsString = JsonSerializer.Serialize(currentBasket);
                await distributedCache.SetStringAsync(cacheKey, basketAsString, token: cancellationToken);
                return ServiceResult.SuccessAsNoContent();
            }
            currentBasket = JsonSerializer.Deserialize<Data.Basket>(basketAsString, new JsonSerializerOptions
            {
                IncludeFields = true,
                PropertyNameCaseInsensitive = true
            });


            var existingBasketItem = currentBasket!.BasketItems.FirstOrDefault(x => x.Id == request.CourseId);


            if (existingBasketItem is not null)
            {
                currentBasket.BasketItems.Remove(existingBasketItem);
            }
            currentBasket.BasketItems.Add(newBasketItem);

            currentBasket.ApplyAvailableDiscountRate();

            basketAsString = JsonSerializer.Serialize(currentBasket);
            await distributedCache.SetStringAsync(cacheKey, basketAsString, token: cancellationToken);
            return ServiceResult.SuccessAsNoContent();
        }
    }
}
