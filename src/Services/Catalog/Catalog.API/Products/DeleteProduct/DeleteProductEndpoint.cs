namespace Catalog.API.Products.DeleteProduct
{
    public record DeleteProductResponse(bool IsSuccess);

    public class DeleteProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/products/{id}", async (Guid id, ISender sender) =>
            {
                var result = await sender.Send(new DeleteProductCommand(id));

                var response = result.Adapt<DeleteProductResponse>();

                return Results.Ok(response);
            })
                .WithName("DeleteProduct")
                .WithSummary("Delete Product")
                .WithDescription("Delete Product")
                .ProducesProblem(StatusCodes.Status404NotFound)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .Produces<DeleteProductResponse>(StatusCodes.Status201Created);
        }
    }
}