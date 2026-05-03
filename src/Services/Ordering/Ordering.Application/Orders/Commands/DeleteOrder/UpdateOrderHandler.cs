namespace Ordering.Application.Orders.Commands.UpdateOrder
{
    public class UpdateOrderHandler(IApplicationDBContext dbContext) : ICommandHandler<UpdateOrderCommand, UpdateOrderResult>
    {

        public async Task<UpdateOrderResult> Handle(UpdateOrderCommand command, CancellationToken cancellationToken)
        {
            var orderId = OrderId.Of(command.Order.Id);
            var existingOrder = await dbContext.Orders.FindAsync([orderId ], cancellationToken);
            if (existingOrder == null)
            {
                throw new OrderNotFoundException(orderId);
            }
            UpdateOrderWithNewValues(existingOrder, command.Order);
            dbContext.Orders.Update(existingOrder);
            await dbContext.SaveChangesAsync(cancellationToken);
            return new UpdateOrderResult(true);
        }

        private void UpdateOrderWithNewValues(Order existingOrder, OrderDto order)
        {
            var updatedShippingAddress = Address.Of(order.ShippingAddress.FirstName, order.ShippingAddress.LastName, order.ShippingAddress.EmailAddress, order.ShippingAddress.AddressLine, order.ShippingAddress.Country, order.ShippingAddress.State, order.ShippingAddress.ZipCode);
            var updatedBillingAddress = Address.Of(order.BillingAddress.FirstName, order.BillingAddress.LastName, order.BillingAddress.EmailAddress, order.BillingAddress.AddressLine, order.BillingAddress.Country, order.BillingAddress.State, order.BillingAddress.ZipCode);
            var updatedPayment = order.Payment != null ? Payment.Of(order.Payment.CardNumber, order.Payment.CardName, order.Payment.CardHolderName, order.Payment.Expiration, order.Payment.Cvv, order.Payment.PaymentMethod) : null;
            existingOrder.Update(
                orderName: OrderName.Of(order.OrderName),
                shippingAddress: updatedShippingAddress,
                billingAddress: updatedBillingAddress,
                payment: updatedPayment,
                status: order.Status
                );
        }
    }
}
