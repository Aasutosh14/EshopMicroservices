
namespace Ordering.Application.Orders.Queries.GetOrdersByCustomer
{
    public class GetOrdersByCustomerHandler(IApplicationDBContext dbcontext) : IQueryHandler<GetOrdersByCustomerQuery, GetOrdersByCustomerResult>
    {
        public async Task<GetOrdersByCustomerResult> Handle(GetOrdersByCustomerQuery query, CancellationToken cancellationToken)
        {
            var orders = await dbcontext.Orders.Include(o => o.OrderItems).Where(o => o.CustomerId == CustomerId.Of(query.customerId)).OrderBy(o => o.OrderName.Value).ToListAsync(cancellationToken);

            return new GetOrdersByCustomerResult(orders.ToOrderDtoList());
        }

    }
}
