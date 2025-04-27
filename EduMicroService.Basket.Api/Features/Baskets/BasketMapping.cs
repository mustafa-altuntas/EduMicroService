using AutoMapper;
using EduMicroService.Basket.Api.Data;

namespace EduMicroService.Basket.Api.Features.Baskets
{
    public class BasketMapping : Profile
    {
        public BasketMapping()
        {
            CreateMap<Data.Basket, Dto.BasketDto>();
            CreateMap<BasketItem, Dto.BasketItemDto>();
        }
    }
}
