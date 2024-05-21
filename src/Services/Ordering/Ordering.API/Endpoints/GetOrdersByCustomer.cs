using Ordering.Application.Orders.Queries.GetOrdersByCustomer;

public record GetOrdersByCustomerResponse(IEnumerable<OrderDto> Orders);
public class GetOrdersByCustomer : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/orders/customer/{customerId}",
            async (Guid customerId, ISender sender) =>
            {
                var result =await sender.Send(new GetOrdersByCustomerQuery(customerId));

                var response = result.Adapt<GetOrdersByCustomerResponse>();

                return Results.Ok(response);
            })
                .WithName("GetOrdersByCustomer")
                .WithSummary("Get Orders By Customer")
                .WithDescription("Get Orders By Customer")
                .ProducesProblem(StatusCodes.Status404NotFound)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .Produces<GetOrdersByCustomerResponse>(StatusCodes.Status201Created);
    }
}