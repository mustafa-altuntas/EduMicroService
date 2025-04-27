using EduMicroService.Basket.Api.Data;
using EduMicroService.Shared;
using EduMicroService.Shared.Services;
using MediatR;
using System.Text.Json;

namespace EduMicroService.Basket.Api.Features.Baskets.AddBasketItem
{
    public class AddBasketItemCommandHandler(IIdentityService identityService, BasketService basketService)
         : IRequestHandler<AddBasketItemCommand, ServiceResult>
    {
        public async Task<ServiceResult> Handle(AddBasketItemCommand request, CancellationToken cancellationToken)
        {

            var basketAsJson = await basketService.GetBasketFromCacheAsync(cancellationToken);


            Data.Basket? currentBasket;

            var newBasketItem = new BasketItem(request.CourseId, request.CourseName, request.ImageUrl,
                request.CoursePrice, null);


            if (string.IsNullOrEmpty(basketAsJson))
            {
                currentBasket = new Data.Basket(identityService.GetUserId, [newBasketItem]);
                await basketService.CreateBasketToCacheAsync(currentBasket, cancellationToken);
                return ServiceResult.SuccessAsNoContent();
            }
            currentBasket = JsonSerializer.Deserialize<Data.Basket>(basketAsJson, new JsonSerializerOptions
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

            await basketService.CreateBasketToCacheAsync(currentBasket, cancellationToken);
            return ServiceResult.SuccessAsNoContent();
        }
    }
}
