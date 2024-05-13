namespace Basket.API.Basket.DeleteBasket
{
    public record DeleteBasketResponse(bool IsSuccess);

    public class DeleteBasketEndpoints : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/basket/{userName}",
                async (string username, ISender sender) =>
                {
                    var result = await sender.Send(new DeleteBasketCommand(username));

                    var response = result.Adapt<DeleteBasketResponse>();

                    return Results.Ok(response);
                })
                .WithName("DeleteProduct")
                .WithSummary("Delete Product")
                .WithDescription("Delete Product")
                .ProducesProblem(StatusCodes.Status404NotFound)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .Produces<DeleteBasketResponse>(StatusCodes.Status201Created);
        }
    }
}