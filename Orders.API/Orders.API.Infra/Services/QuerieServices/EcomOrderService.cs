using Orders.API.Domain.Entities.Order;
using Orders.API.Domain.Interfaces.Repository.Order;
using Orders.API.Domain.Interfaces.Services;
using Orders.API.Domain.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.API.Application.Services.QuerieServices
{
    public class EcomOrderService : IEcomOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public EcomOrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public IList<EcomOrder> GetOrdersByFilter(GetOrdersFilter filter)
        {
            var response = _orderRepository.GetAll();

            return response;
        }
    }
}
