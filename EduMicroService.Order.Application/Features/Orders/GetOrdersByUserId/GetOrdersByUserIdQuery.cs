using EduMicroService.Shared;

namespace EduMicroService.Order.Application.Features.Orders.GetOrdersByUserId
{
    public record GetOrdersByUserIdQuery : IRequestByServiceResult<List<GetOrdersByUserIdResponse>>;

}
