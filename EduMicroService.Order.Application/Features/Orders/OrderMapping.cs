using AutoMapper;
using EduMicroService.Order.Application.Features.Orders.Create;
using EduMicroService.Order.Domain.Entities;

namespace EduMicroService.Order.Application.Features.Orders
{
    public class OrderMapping : Profile
    {
        public OrderMapping()
        {
            CreateMap<OrderItem, OrderItemDto>()
                .ReverseMap()
                ;
        }
    }
}
