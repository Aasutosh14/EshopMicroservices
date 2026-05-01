using BuildingBlocks.Pagination;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ordering.Application.Orders.Queries.GetOrders
{
    public record GetOrdersQuery(PaginatedRequest PaginatedRequest) : IQuery<GetOrdersResult>;
    public record GetOrdersResult(PaginatedResult<OrderDto> Orders);
}
