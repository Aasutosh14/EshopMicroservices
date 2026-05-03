using Ordering.Application.Orders.Commands.DeleteOrder;

namespace Ordering.API.Endpoints
{
    public record DeleteOrderResponse(bool IsSuccessful);
    public class DeleteOrder : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/orders/{id}", async (Guid id, ISender mediator) =>
            {
                var result = await mediator.Send(new DeleteOrderCommand(id));
                var response = result.Adapt<DeleteOrderResponse>();
                return Results.Ok(response);
            }).WithName("DeleteOrder").WithTags("Orders")
            .Produces<DeleteOrderResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Deletes an order.")
            .WithDescription("Deletes an order with the provided ID.");
        }
    }
}