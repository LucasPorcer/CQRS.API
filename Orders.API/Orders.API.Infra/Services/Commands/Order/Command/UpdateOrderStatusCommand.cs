using Orders.API.Application.Services.Commands.BaseCommand;
using Orders.API.Domain.Enums;

namespace Orders.API.Infra.Services.Commands.Order.Command
{
    public class UpdateOrderStatusCommand : ICommand
    {
        public long OrderNumber { get; set; }
        public OrderStatus Status { get; set; }
    }
}
