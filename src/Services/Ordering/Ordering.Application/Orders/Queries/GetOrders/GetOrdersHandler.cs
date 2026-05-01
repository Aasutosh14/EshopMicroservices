

namespace Ordering.Application.Orders.Queries.GetOrders
{
    public class GetOrdersHandler(IApplicationDBContext dbcontext) : IQueryHandler<GetOrdersQuery, GetOrdersResult>
    {
        public async Task<GetOrdersResult> Handle(GetOrdersQuery query, CancellationToken cancellationToken)
        {
            var pageIndex = query.PaginatedRequest.PageIndex;
            var pageSize = query.PaginatedRequest.PageSize;
            var totalCount = await dbcontext.Orders.LongCountAsync(cancellationToken);
            var orders = await dbcontext.Orders.Include(o => o.OrderItems).OrderBy(o => o.OrderName.Value).Skip(pageSize * pageIndex).Take(pageSize).ToListAsync(cancellationToken);

            return new GetOrdersResult(new BuildingBlocks.Pagination.PaginatedResult<OrderDto>
                (pageIndex,pageSize,totalCount,orders.ToOrderDtoList()
                ));
        }

    }
}
