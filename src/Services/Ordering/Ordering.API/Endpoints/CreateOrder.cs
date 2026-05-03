namespace Ordering.API.Endpoints
{
    public record CreateOrderRequest(OrderDto Order);
    public record CreateOrderResponse(Guid Id);
    public class CreateOrder : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/orders", async (CreateOrderRequest request, ISender mediator) =>
            {
                var command = request.Adapt<CreateOrderCommand>();
                var orderId = await mediator.Send(command);
                var response = request.Adapt<CreateOrderResponse>();
                return Results.Created($"/orders/{response.Id}", response);
            }).WithName("CreateOrder").WithTags("Orders")
            .Produces<CreateOrderResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Creates a new order.")
            .WithDescription("Creates a new order with the provided details.");
        }
    }
}
