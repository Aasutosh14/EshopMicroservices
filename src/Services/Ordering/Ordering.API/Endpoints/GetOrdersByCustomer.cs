using Ordering.Application.Orders.Queries.GetOrdersByCustomer;
using Ordering.Application.Orders.Queries.GetOrdersByName;

namespace Ordering.API.Endpoints
{
    public record GetOrdersByCustomerResponse(IEnumerable<OrderDto> Orders);

    public class GetOrdersByCustomer : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/orders/customer/{customerId}", async (string customerId, ISender mediator) =>
            {
                var result = await mediator.Send(new GetOrdersByCustomerQuery(Guid.Parse(customerId)));
                var response = result.Adapt<GetOrdersByCustomerResponse>();
                return Results.Ok(response);
            }).WithName("GetOrdersByCustomer").WithTags("Orders")
            .Produces<GetOrdersByCustomerResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Gets orders by name.")
            .WithDescription("Gets orders with the provided name.");
        }
    }
}
