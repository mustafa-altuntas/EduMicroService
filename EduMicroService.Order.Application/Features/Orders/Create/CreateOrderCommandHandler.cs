using EduMicroService.Order.Application.Contracts.Repositories;
using EduMicroService.Order.Application.Contracts.UnitOfWork;
using EduMicroService.Order.Domain.Entities;
using EduMicroService.Shared;
using EduMicroService.Shared.Services;
using MediatR;

namespace EduMicroService.Order.Application.Features.Orders.Create
{
    public class CreateOrderCommandHandler(IOrderRepository orderRepository, IGenericRepository<int, Address> addresRepository, IIdentityService identityService, IUnitOfWork unitOfWork) : IRequestHandler<CreateOrderCommand, ServiceResult>
    {
        public async Task<ServiceResult> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {

            if (!request.Items.Any())
            {
                return ServiceResult.Error("Order items are empty", "Order must have at least one item", System.Net.HttpStatusCode.BadRequest);
            }

            var newAddress = new Address()
            {
                District = request.Address.District,
                Line = request.Address.Line,
                Province = request.Address.Province,
                Street = request.Address.Street,
                ZipCode = request.Address.ZipCode
            };


            var order = Domain.Entities.Order.CreateUnPaidOrder(identityService.GetUserId, request.DiscountRate);
            request.Items.ForEach(item => { order.AddOrderItem(item.ProductId, item.ProductName, item.UnitPrice); });


            order.Address = newAddress;



            orderRepository.Add(order);
            await unitOfWork.CommitAsync(cancellationToken);


            //Todo:Payment işlemleri yapılacak
            var paymentId = Guid.Empty;


            order.SetPaidStatus(paymentId);

            orderRepository.Update(order);
            await unitOfWork.CommitAsync(cancellationToken);

            return ServiceResult.SuccessAsNoContent();

        }
    }
}
