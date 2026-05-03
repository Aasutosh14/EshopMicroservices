using Ordering.Application.Orders.Queries.GetOrdersByName;

namespace Ordering.API.Endpoints
{
    public record GetOrdersByNameResponse(IEnumerable<OrderDto> Orders);
    public class GetOrdersByName : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/orders/{orderName}", async (string orderName, ISender mediator) =>
            {
                var result = await mediator.Send(new GetOrdersByNameQuery(orderName));
                var response = result.Adapt<GetOrdersByNameResponse>();
                return Results.Ok(response);
            }).WithName("GetOrdersByName").WithTags("Orders")
            .Produces<GetOrdersByNameResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Gets orders by name.")
            .WithDescription("Gets orders with the provided name.");
        }
    }
}