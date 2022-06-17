using Orders.API.Domain.Enums;
using System;

namespace Orders.API.Application.Services.Dto
{
    public class EcomOrderDto
    {
        public long OrderNumber { get; set; }
        public string OrderIdentifier { get; set; }
        public DateTime CreatedAt { get; set; }
        public OrderStatus Status { get; set; }
        public OrderItemDto[] OrderItems { get; set; }
        public string OrderInternalNumber => $"{OrderNumber}_{OrderIdentifier}";
    }
}
