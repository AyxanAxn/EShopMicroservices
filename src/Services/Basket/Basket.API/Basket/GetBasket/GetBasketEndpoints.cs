namespace Basket.API.Basket.GetBasket
{
    public record GetBasketResponse(ShoppingCart cart);

    public class GetBasketEndpoints : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/basket/{username}",
                async (string userName, ISender sender) =>
                {
                    var result = await sender.Send(new GetBasketQuery(userName));

                    var response = result.Adapt<GetBasketResponse>();

                    return Results.Ok(response);
                })
            .WithName("GetBasket")
            .WithSummary("Get Basket")
            .WithDescription("Get Basket")
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .Produces<GetBasketResponse>(StatusCodes.Status201Created);
        }
    }
}