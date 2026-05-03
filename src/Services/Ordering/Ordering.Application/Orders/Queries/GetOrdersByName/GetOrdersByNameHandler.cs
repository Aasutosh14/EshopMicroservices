using Ordering.Application.Extensions;


namespace Ordering.Application.Orders.Queries.GetOrdersByName
{
    public class GetOrdersByNameHandler(IApplicationDBContext dbcontext) : IQueryHandler<GetOrdersByNameQuery, GetOrdersByNameResult>
    {
        public async Task<GetOrdersByNameResult> Handle(GetOrdersByNameQuery query, CancellationToken cancellationToken)
        {
            var orders = await dbcontext.Orders.Include(o => o.OrderItems).Where(o => o.OrderName.Value.Contains(query.orderName)).OrderBy(o => o.OrderName.Value).ToListAsync(cancellationToken);
            
            return new GetOrdersByNameResult(orders.ToOrderDtoList());
        }
    }
}
