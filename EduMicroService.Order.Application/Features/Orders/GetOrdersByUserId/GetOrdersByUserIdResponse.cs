using EduMicroService.Order.Application.Features.Orders.Create;

namespace EduMicroService.Order.Application.Features.Orders.GetOrdersByUserId;
public record GetOrdersByUserIdResponse(DateTime Created, decimal TotalPrice, List<OrderItemDto> Items);
