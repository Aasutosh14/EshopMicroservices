namespace Ordering.Application.Orders.Commands.CreateOrder
{
    public record UpdateOrderCommand(OrderDto Order) : ICommand<CreateOrderResult>;
    public record CreateOrderResult(Guid Id);
    public class CreateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
    {
        public CreateOrderCommandValidator()
        {
            RuleFor(c => c.Order.CustomerId).NotNull().WithMessage("Customer ID is required.");
            RuleFor(c => c.Order.OrderName).NotEmpty().WithMessage("Order name is required.");
            RuleFor(c => c.Order.OrderItems).NotEmpty().WithMessage("Order must have at least one item.");
        }

    }
}
