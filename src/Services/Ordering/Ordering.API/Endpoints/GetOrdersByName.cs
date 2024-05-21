using Ordering.Application.Orders.Queries.GetOrdersByName;

namespace Ordering.API.Endpoints
{

    public record GetOrdersByNameResponse(IEnumerable<OrderDto> Orders);
    public class GetOrdersByName : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/orders/{orderName}",
                async (string orderName, ISender sender) =>
                {
                    var result = await sender.Send(new GetOrdersByNameQuery(orderName));

                    var response = result.Adapt<GetOrdersByNameResponse>();

                    return Results.Ok(response);
                })
                .WithName("GetOrder")
                .WithSummary("Get Order")
                .WithDescription("Get Order")
                .ProducesProblem(StatusCodes.Status404NotFound)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .Produces<GetOrdersByNameResponse>(StatusCodes.Status201Created); ;

        }
    }
}