using MediatR;
using Microsoft.Extensions.Logging;
using Orders.API.Application.Services.Commands.BaseCommand;
using Orders.API.Domain.Entities.Order;
using Orders.API.Domain.Interfaces.Repository.Order;
using Orders.API.Domain.Notifications;
using Orders.API.Infra.Services.Commands.Order.Command;
using Orders.API.Infra.Services.Validators.Order;
using System.Threading;
using System.Threading.Tasks;

namespace Orders.API.Infra.Services.Commands.Order
{
    public class OrderCommandHandler : IRequestHandler<CreateOrderCommand, CommandResponse>,
        IRequestHandler<RemoveOrderCommand, CommandResponse>,
        IRequestHandler<UpdateOrderStatusCommand, CommandResponse>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ILogger<OrderCommandHandler> _logger;
        private readonly DomainNotification _domainNotification;
        public OrderCommandHandler(IOrderRepository orderRepository,
             ILogger<OrderCommandHandler> logger,
             DomainNotification domainNotification)
        {
            _orderRepository = orderRepository;
            _logger = logger;
            _domainNotification = domainNotification;
        }

        public async Task<CommandResponse> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var ecomOrder = new EcomOrder().Create(request.OrderNumber, request.OrderIdentifier, request.OrderItems);

                _orderRepository.Add(ecomOrder);

                return CommandResponse.BuildResponse(_domainNotification);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message);
                return CommandResponse.BuildResponse(_domainNotification);
            }
        }

        public async Task<CommandResponse> Handle(RemoveOrderCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var order = _orderRepository.GetById(request.OrderNumber);

                _domainNotification.AddNotification(new RemoveOrderCommandValidator().Validate(order));

                if (_domainNotification.IsValid)
                    _orderRepository.Remove(order);

                return CommandResponse.BuildResponse(_domainNotification);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message);
                return CommandResponse.BuildResponse(_domainNotification);
            }
        }

        public async Task<CommandResponse> Handle(UpdateOrderStatusCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var order = _orderRepository.GetById(request.OrderNumber);

                _domainNotification.AddNotification(new UpdateOrderStatusCommandValidator().Validate(order));

                if (_domainNotification.IsValid)
                {
                    order.SetOrderStatus(request.Status);

                    _orderRepository.Update(order);

                    return CommandResponse.BuildResponse(_domainNotification);
                }

                return CommandResponse.BuildResponse(_domainNotification);

            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message);
                return CommandResponse.BuildResponse(_domainNotification);
            }
        }
    }
}
