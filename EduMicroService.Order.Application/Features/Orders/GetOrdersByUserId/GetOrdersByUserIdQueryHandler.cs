using AutoMapper;
using EduMicroService.Order.Application.Contracts.Repositories;
using EduMicroService.Order.Application.Features.Orders.Create;
using EduMicroService.Shared;
using EduMicroService.Shared.Services;
using MediatR;

namespace EduMicroService.Order.Application.Features.Orders.GetOrdersByUserId
{
    public class GetOrdersByUserIdQueryHandler(IOrderRepository orderRepository, IIdentityService identityService, IMapper mapper) : IRequestHandler<GetOrdersByUserIdQuery, ServiceResult<List<GetOrdersByUserIdResponse>>>
    {
        public async Task<ServiceResult<List<GetOrdersByUserIdResponse>>> Handle(GetOrdersByUserIdQuery request, CancellationToken cancellationToken)
        {
            var orders = await orderRepository.GetOrdersByUserId(identityService.GetUserId);
            var response = orders.Select(o => new GetOrdersByUserIdResponse(o.Created, o.TotalPrice, mapper.Map<List<OrderItemDto>>(o.OrderItems))).ToList();
            return ServiceResult<List<GetOrdersByUserIdResponse>>.SuccessAsOk(response);
        }
    }
}
