using Ordering.Domain.Enums;

namespace Ordering.Application.Dtos
{
    public record OrderDto(Guid Id, Guid CustomerId, string OrderName, AddressDto ShippingAddress, AddressDto BillingAddress, PaymentDto Payment, OrderStatus Status, List<OrderItemDto> OrderItems)
    {
        public OrderDto(Guid Id, Guid CustomerId, string OrderName, AddressDto ShippingAddress, AddressDto BillingAddress, PaymentDto Payment, List<OrderItemDto> OrderItems)
            : this(Id, CustomerId, OrderName, ShippingAddress, BillingAddress, Payment, default(OrderStatus), OrderItems)
        {
        }
    }
}
