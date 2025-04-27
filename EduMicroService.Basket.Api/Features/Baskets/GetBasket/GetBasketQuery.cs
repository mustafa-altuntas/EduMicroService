using EduMicroService.Basket.Api.Dto;
using EduMicroService.Shared;

namespace EduMicroService.Basket.Api.Features.Baskets.GetBasket
{
    public record GetBasketQuery : IRequestByServiceResult<BasketDto>;
}
