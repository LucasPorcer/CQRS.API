using Orders.API.Application.Services.Commands.BaseCommand;
using Orders.API.Domain.Entities.Order;
using Orders.API.Domain.Enums;
using System;

namespace Orders.API.Infra.Services.Commands.Order.Command
{
    public class CreateOrderCommand : ICommand
    {
        public long OrderNumber { get; set; }
        public string OrderIdentifier { get; set; }
        public DateTime CreatedAt { get; set; }
        public OrderStatus Status { get; set; }
        public OrderItem[] OrderItems { get; set; }
    }
}
