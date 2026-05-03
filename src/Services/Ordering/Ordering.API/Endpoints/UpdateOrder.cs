

namespace Ordering.API.Endpoints
{
    public record UpdateOrderRequest(OrderDto Order);
    public record UpdateOrderResponse(bool IsSuccessful);

    public class UpdateOrder : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/orders", async (UpdateOrderRequest request,ISender mediator) =>
            {
                var command = request.Adapt<UpdateOrderCommand>();
                var result = await mediator.Send(command);
                var response = result.Adapt<UpdateOrderResponse>();
                return Results.Ok(response);
            }).WithName("UpdateOrder").WithTags("Orders")
            .Produces<CreateOrderResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Update  order.")
            .WithDescription("Update  order"); ;
        }
    }
}
