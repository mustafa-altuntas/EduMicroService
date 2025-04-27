using EduMicroService.Shared;

namespace EduMicroService.Basket.Api.Features.Baskets.DeleteBasketItem
{
    public record DeleteBasketItemCommand(Guid Id):IRequestByServiceResult;
}
