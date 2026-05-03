using System;
using System.Collections.Generic;
using System.Text;

namespace Ordering.Application.Orders.Queries.GetOrdersByName
{
    public record GetOrdersByNameQuery(string orderName) : IQuery<GetOrdersByNameResult>;
    public record GetOrdersByNameResult(IEnumerable<OrderDto> Orders);
}
