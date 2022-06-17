using Orders.API.Domain.Entities.Order;
using Orders.API.Domain.Interfaces.Repository.Order;
using Orders.API.InfraData.Database.Order;

namespace Orders.API.InfraData.Repositories.Order
{
    public class OrderRepository : DatabaseRepositoryBase<EcomOrder>, IOrderRepository
    {
        public OrderRepository(OrderContext context) : base (context)
        {
            
        }
    }
}

