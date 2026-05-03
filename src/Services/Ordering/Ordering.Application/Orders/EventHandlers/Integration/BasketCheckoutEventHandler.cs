using BuildingBlocks.Messaging.Events;
using MassTransit;
using Ordering.Application.Orders.Commands.CreateOrder;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ordering.Application.Orders.EventHandlers.Integration
{
    public class BasketCheckoutEventHandler(ILogger<BasketCheckoutEventHandler> logger, ISender sender) : IConsumer<BasketCheckoutEvent>
    {
        public async Task Consume(ConsumeContext<BasketCheckoutEvent> context)
        {
            logger.LogInformation("Received BasketCheckoutEvent for User: {UserName}, TotalPrice: {TotalPrice}", context.Message.UserName, context.Message.TotalPrice);
            var command = MapToCreateOrderCommnd(context.Message);
            await sender.Send(command);
        }

        private CreateOrderCommand MapToCreateOrderCommnd(BasketCheckoutEvent message)
        {
            // Map the BasketCheckoutEvent to CreateOrderCommand
            var result =  new OrderDto
                             (
                                Id:Guid.NewGuid(),
                                  CustomerId: message.CustomerId,
                                  OrderName: message.UserName,
                                  ShippingAddress: new AddressDto(
                                      message.FirstName,
                                      message.LastName,
                                      message.EmailAddress,
                                       message.AddressLine,
                                      message.Country,
                                      message.State, message.ZipCode)
                                  ,
                                 BillingAddress: new AddressDto(
                                      message.FirstName,
                                      message.LastName,
                                      message.EmailAddress,
                                       message.AddressLine,
                                      message.Country,
                                      message.State, message.ZipCode)
                                  ,
                                 Payment: new PaymentDto(
                                     message.CardName, message.CardNumber, message.CardHolderName, message.Expiration, message.CVV, message.PaymentMethod),
                                 OrderItems: new List<OrderItemDto>() // You would need to populate this list based on the items in the basket
                             );
            return new CreateOrderCommand(result);
        }
    }
}
