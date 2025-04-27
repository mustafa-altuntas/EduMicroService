using EduMicroService.Shared;
using MediatR;
using System.Text.Json;

namespace EduMicroService.Basket.Api.Features.Baskets.DeleteBasketItem
{
    public class DeleteBasketItemCommandHandler(BasketService basketService) : IRequestHandler<DeleteBasketItemCommand, ServiceResult>
    {
        public async Task<ServiceResult> Handle(DeleteBasketItemCommand request, CancellationToken cancellationToken)
        {
            var basketAsString = await basketService.GetBasketFromCacheAsync(cancellationToken);

            if (string.IsNullOrEmpty(basketAsString))
            {
                return ServiceResult.ErrorAsNotFound();
            }


            Data.Basket? currentBasket;
            currentBasket = JsonSerializer.Deserialize<Data.Basket>(basketAsString);

            var basketItemToDelete = currentBasket!.BasketItems.FirstOrDefault(x => x.Id == request.Id);
            if (basketItemToDelete is null)
            {
                return ServiceResult.ErrorAsNotFound();
            }

            currentBasket.BasketItems.Remove(basketItemToDelete);
            await basketService.CreateBasketToCacheAsync(currentBasket, cancellationToken);

            return ServiceResult.SuccessAsNoContent();







        }
    }
}



