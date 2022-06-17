using Orders.API.Domain.Entities.Order;
using Orders.API.Domain.Queries;
using System.Collections.Generic;

namespace Orders.API.Domain.Interfaces.Services
{
    public interface IEcomOrderService
    {
        IList<EcomOrder> GetOrdersByFilter(GetOrdersFilter filter);
    }
}
