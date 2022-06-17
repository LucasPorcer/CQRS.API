using Orders.API.Application.Services.Commands.BaseCommand;

namespace Orders.API.Infra.Services.Commands.Order.Command
{
    public class RemoveOrderCommand : ICommand
    {
        public long OrderNumber { get; set; }
    }
}
