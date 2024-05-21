
using Catalog.API.Products.CreateProduct;

namespace Catalog.API.Products.GetProducts;

public record GetProductsResponse(IEnumerable<Product> Products);

public class GetProductsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.Map("/products", async (ISender sender) =>
        {
            var result = await sender.Send(new GetProductQuery());

            var response = result.Adapt<GetProductsResponse>();

            return Results.Ok(response);
        })
         .WithName("GetProductProducts")
         .Produces<GetProductsResponse>(StatusCodes.Status200OK)
         .ProducesProblem(StatusCodes.Status400BadRequest)
         .WithSummary("Create Product")
         .WithDescription("Get all products for ecommerce microsservices"); 
    }
}
