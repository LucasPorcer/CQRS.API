using FluentValidation;
using Orders.API.Domain.Entities.Order;

namespace Orders.API.Infra.Services.Validators.Order
{
    public class OrderValidator : AbstractValidator<EcomOrder>
    {
    }

    public class RemoveOrderCommandValidator : AbstractValidator<EcomOrder>
    {
        public RemoveOrderCommandValidator()
        {
            RuleFor(c => c)
                .NotNull();

            RuleFor(c => c.Status)
               .NotEqual(Domain.Enums.OrderStatus.Finished);
        }
    }
    public class UpdateOrderStatusCommandValidator : AbstractValidator<EcomOrder>
    {
        public UpdateOrderStatusCommandValidator()
        {
            RuleFor(c => c)
              .NotNull();

            RuleFor(c => c.Status)
                .NotEqual(Domain.Enums.OrderStatus.Finished);
        }
    }

}
