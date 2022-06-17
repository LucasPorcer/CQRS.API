using Orders.API.Domain.Enums;
using System;

namespace Orders.API.Domain.Entities.Order
{
    public class EcomOrder
    {
        public long OrderNumber { get; private set; }
        public string OrderIdentifier { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public OrderStatus Status { get; private set; } = OrderStatus.Created;
        public OrderItem[] OrderItems { get; private set; }
        public EcomOrder Create(long orderNumber, string orderIdentifier, OrderItem[] items)
        {
            return new EcomOrder
            {
                OrderNumber = orderNumber,
                OrderIdentifier = orderIdentifier,
                CreatedAt = DateTime.UtcNow,
                OrderItems = items

            };
        }

        public void SetOrderStatus(OrderStatus newstatus)
        {
            Status = newstatus;
        }
    }
}
