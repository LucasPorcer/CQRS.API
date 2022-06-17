using Orders.API.Infra.Services.Commands.Order;
using Moq;
using System.Threading.Tasks;
using Xunit;
using Orders.API.Infra.Services.Commands.Order.Command;
using Orders.API.Domain.Notifications;
using System.Threading;
using Moq.AutoMock;
using Orders.API.Domain.Interfaces.Repository.Order;
using Orders.API.Domain.Entities.Order;
using FluentAssertions;

namespace Orders.API.Test.Unit.HandlerTest
{
    public class OrderCommandHandlerTest : BaseUnitTests
    {
        private readonly AutoMocker _mocker;
        private OrderCommandHandler _handler;

        public OrderCommandHandlerTest()
        {
            _mocker = new AutoMocker();
        }

        [Fact(DisplayName = "Create Order - Should Create Order Correctly")]
        public async Task Handle_ShouldCreateOrderCorrectly()
        {
            // Arrange
            var command = new CreateOrderCommand
            {
                OrderNumber = 1,
                OrderIdentifier = "Test",
                OrderItems = null
            };

            var ecomOrder = new EcomOrder().Create(1, "test", null);

            CreateHandleInstance();

            _mocker.GetMock<IOrderRepository>().Setup(p => p.Add(It.IsAny<EcomOrder>())).Returns(ecomOrder);

            // Act
            await _handler.Handle(command, new CancellationToken());

            // Assert
            _mocker.GetMock<DomainNotification>().Object.IsValid.Should().BeTrue();
        }

        [Fact(DisplayName = "Remove Order - Should Remove Order Correctly")]
        public async Task Handle_ShouldRemoveOrderCorrectly()
        {
            // Arrange
            var command = new RemoveOrderCommand
            {
                OrderNumber = 1
            };

            var ecomOrder = new EcomOrder().Create(1, "test", null);

            CreateHandleInstance();

            _mocker.GetMock<IOrderRepository>().Setup(p => p.Remove(It.IsAny<EcomOrder>())).Returns(ecomOrder);

            // Act
            await _handler.Handle(command, new CancellationToken());

            // Assert
            _mocker.GetMock<DomainNotification>().Object.IsValid.Should().BeTrue();
        }

        [Fact(DisplayName = "Remove Order - Should Not Remove An Finished Order")]
        public async Task Handle_ShouldNotRemoveAnFinishedOrder()
        {
            // Arrange
            var command = new RemoveOrderCommand
            {
                OrderNumber = 1
            };

            var ecomOrder = new EcomOrder().Create(1, "test", null);
            ecomOrder.SetOrderStatus(Domain.Enums.OrderStatus.Finished);

            CreateHandleInstance();

            _mocker.GetMock<IOrderRepository>().Setup(p => p.GetById(It.IsAny<long>())).Returns(ecomOrder);

            // Act
            await _handler.Handle(command, new CancellationToken());

            // Assert
            _mocker.GetMock<DomainNotification>().Object.IsValid.Should().BeFalse();
        }

        [Fact(DisplayName = "Update Order Status - Should Update Order Status Correctly")]
        public async Task Handle_ShouldUpdateOrderCorrectly()
        {
            // Arrange
            var command = new UpdateOrderStatusCommand
            {
                OrderNumber = 1,
                Status = Domain.Enums.OrderStatus.Pending
            };

            var ecomOrder = new EcomOrder().Create(1, "test", null);

            CreateHandleInstance();

            _mocker.GetMock<IOrderRepository>().Setup(p => p.Update(It.IsAny<EcomOrder>()));

            // Act
            await _handler.Handle(command, new CancellationToken());

            // Assert
            _mocker.GetMock<DomainNotification>().Object.IsValid.Should().BeTrue();
        }

        #region PrivateMethods

        private void CreateHandleInstance() =>
            _handler = _mocker.CreateInstance<OrderCommandHandler>();

        #endregion

    }
}
