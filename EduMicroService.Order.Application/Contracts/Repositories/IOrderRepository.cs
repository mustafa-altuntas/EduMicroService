namespace EduMicroService.Order.Application.Contracts.Repositories
{
    public interface IOrderRepository: IGenericRepository<Guid,Domain.Entities.Order>
    {
        Task<List<Domain.Entities.Order>> GetOrdersByUserId(Guid buyerId);
    }
}
