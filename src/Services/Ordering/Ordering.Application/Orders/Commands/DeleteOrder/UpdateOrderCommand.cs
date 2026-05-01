namespace Ordering.Application.Orders.Commands.UpdateOrder
{
    public record DeleteOrderCommand(OrderDto Order) : ICommand<UpdateOrderResult>;
    public record UpdateOrderResult(bool IsSuccessful);
    public class UpdateOrderCommandValidator : AbstractValidator<DeleteOrderCommand>
    {
        public UpdateOrderCommandValidator()
        {
            RuleFor(c => c.Order.CustomerId).NotNull().WithMessage("Customer ID is required.");
            RuleFor(c => c.Order.OrderName).NotEmpty().WithMessage("Order name is required.");
            RuleFor(c => c.Order.OrderItems).NotEmpty().WithMessage("Order must have at least one item.");
        }

    }
}
