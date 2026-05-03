using BuildingBlocks.CQRS;
using BuildingBlocks.Pagination;
using Microsoft.AspNetCore.Mvc;
using Ordering.Application.Orders.Queries.GetOrders;
using Ordering.Application.Orders.Queries.GetOrdersByCustomer;

namespace Ordering.API.Endpoints
{
    //public record GetOrdersRequest([property: FromQuery] PaginatedRequest PaginatedRequest);
    public record GetOrdersResponse(PaginatedResult<OrderDto> Orders);
    public class GetOrders : ICarterModule
    {        
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/orders", async ([AsParameters] PaginatedRequest request, ISender mediator) =>
            {
                var result = await mediator.Send(new GetOrdersQuery(request));
                var response = result.Adapt<GetOrdersResponse>();
                return Results.Ok(response);
            }).WithName("GetOrders").WithTags("Orders")
            .Produces<GetOrdersResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Gets orders.")
            .WithDescription("Gets order");
        }
    }
}
