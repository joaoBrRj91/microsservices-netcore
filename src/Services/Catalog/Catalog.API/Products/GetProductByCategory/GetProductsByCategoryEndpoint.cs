namespace Catalog.API.Products.GetProductByCategory;

//public record GetProductsByCategoryRequest()
public record GetProductsByCategoryResponse(IEnumerable<Product> Product);

public class GetProductsByCategoryEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/category/{category}", async (string category, ISender sender) => { 
        
            var result = await sender.Send(new GetProductByCategoryQuery(category));

            var response = result.Adapt<GetProductsByCategoryResponse>();

            return Results.Ok(response);

        })
         .WithName("GetProductByCategory")
         .Produces<GetProductsByCategoryResponse>(StatusCodes.Status200OK)
         .ProducesProblem(StatusCodes.Status400BadRequest)
         .WithSummary("Get Product By Category")
         .WithDescription("Get all products by category for ecommerce microsservices");
    }
}
