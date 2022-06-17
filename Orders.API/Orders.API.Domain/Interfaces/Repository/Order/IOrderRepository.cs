using Orders.API.Domain.Entities.Order;
using System;
using System.Threading.Tasks;

namespace Orders.API.Domain.Interfaces.Repository.Order
{
    public interface IOrderRepository : IDatabaseRepositoryBase<EcomOrder>
    {
        
    }
}
