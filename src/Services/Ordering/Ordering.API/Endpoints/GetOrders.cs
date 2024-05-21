using Ordering.Application.Orders.Queries.GetOrders;
using BuildingBlocks.Pagination;

namespace Ordering.API.Endpoints
{
    public record GetOrdersResponse(PaginatedResult<OrderDto> Orders);
    public class GetOrders : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/orders", async ([AsParameters] PaginationRequest request, ISender sender) =>
            {
                var result = await sender.Send(new GetOrdersQuery(request));

                var response = result.Adapt<GetOrdersResponse>();

                return Results.Ok(response);
            })
                .WithName("GetOrders")
                .WithSummary("Get Orders")
                .WithDescription("Get Orders")
                .ProducesProblem(StatusCodes.Status404NotFound)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .Produces<GetOrdersResponse>(StatusCodes.Status200OK);
        }
    }
}