using Catalog.API.Products.CreateProduct;

namespace Catalog.API.Products.GetProducts
{
    public record GetProductsResponse(IEnumerable<Product> Products);
    public class GetProductsEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products", async (ISender sender) =>
            {
                var result = await sender.Send(new GetProductsQuery());

                var response = result.Adapt<GetProductsResponse>();

                return Results.Ok(response);
            })
            .WithName("GetProduct")
            .WithSummary("Get Product")
            .WithDescription("Get Product")
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .Produces<CreateProductResponse>(StatusCodes.Status201Created);
        }
    }
}
