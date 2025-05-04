using EduMicroService.Order.Application.Contracts.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EduMicroService.Order.Persistence.Repositories
{
    public class OrderRepository(AppDbContext context) : GenericRepository<Guid, Domain.Entities.Order>(context), IOrderRepository
    {
        public async Task<List<Domain.Entities.Order>> GetOrdersByUserId(Guid buyerId)
        {
            var orders =  await context.Orders
                .Include(x => x.OrderItems)
                //.Include(x => x.Address)
                .Where(x => x.BuyerId == buyerId)
                .OrderByDescending(x => x.Created)
                .ToListAsync();

            if(orders is null)
                return new List<Domain.Entities.Order>();

            return orders;
        }

    }
     
}
