

namespace Ordering.Application.Orders.Commands.DeleteOrder
{
    public record DeleteOrderCommand(Guid Id) : ICommand<DeleteOrderResult>;
    public record DeleteOrderResult(bool IsSuccessful);
    public class DeleteOrderCommandValidator     : AbstractValidator<DeleteOrderCommand>
    {
        public DeleteOrderCommandValidator()
        {
            RuleFor(c => c.Id).NotEmpty().WithMessage("Order ID is required.");
        }

    }
}
