
using Basket.API.Baskets.GetBasket;

namespace Basket.API.Baskets.StoreBasket;

public record StoreBasketRequest(ShoppingCart Cart);
public record StoreBasketResponse(bool IsSuccess, string UserName);

public class StoreBasketEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/basket", async (StoreBasketRequest request, ISender sender) =>
        {
            var command = request.Adapt<StoreBasketCommand>();

            var result = await sender.Send(command);

            var response = result.Adapt<StoreBasketResponse>();

            return Results.Created($"/basket/{response.UserName}", response);
        })
         .WithName("StoreBasket")
         .Produces<StoreBasketResponse>(StatusCodes.Status201Created)
         .ProducesProblem(StatusCodes.Status500InternalServerError)
         .ProducesProblem(StatusCodes.Status400BadRequest)
         .WithSummary("Create Or Update Basket")
         .WithDescription("Store basket of the user for ecommerce microsservices");
    }
}
