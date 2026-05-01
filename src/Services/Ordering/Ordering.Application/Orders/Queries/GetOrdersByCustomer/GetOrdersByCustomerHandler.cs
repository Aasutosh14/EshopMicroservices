
namespace Ordering.Application.Orders.Queries.GetOrdersByCustomer
{
    public class GetOrdersHandler(IApplicationDBContext dbcontext) : IQueryHandler<GetOrdersQuery, GetOrdersByCustomerResult>
    {
        public async Task<GetOrdersByCustomerResult> Handle(GetOrdersQuery query, CancellationToken cancellationToken)
        {
            var orders = await dbcontext.Orders.Include(o => o.OrderItems).Where(o => o.CustomerId == CustomerId.Of(query.customerId)).OrderBy(o => o.OrderName.Value).ToListAsync(cancellationToken);

            return new GetOrdersByCustomerResult(orders.ToOrderDtoList());
        }

    }
}
