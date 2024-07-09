namespace Basket.API.Baskets.GetBasket;

public record GetBasketQuery(string UserName) : IQuery<GetBasketResult>;
public record GetBasketResult(ShoppingCart Cart);

public class GetBasketQueryHandler : IQueryHandler<GetBasketQuery, GetBasketResult>
{
    public async Task<GetBasketResult> Handle(GetBasketQuery query, CancellationToken cancellationToken)
    {
        //TODO: Get Basket from Redis or Database
        var basket = new GetBasketResult(new ShoppingCart("swn"));

        return await Task.FromResult(basket);
    }
}
