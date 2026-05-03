using System;
using System.Collections.Generic;
using System.Text;

namespace Ordering.Application.Orders.Queries.GetOrdersByCustomer
{
    public record GetOrdersByCustomerQuery(Guid customerId) : IQuery<GetOrdersByCustomerResult>;
    public record GetOrdersByCustomerResult(IEnumerable<OrderDto> Orders);
}
