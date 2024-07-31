using Basket.API.Data;

namespace Basket.API.Baskets.GetBasket;

public record GetBasketQuery(string UserName) : IQuery<GetBasketResult>;
public record GetBasketResult(ShoppingCart Cart);

public class GetBasketQueryHandler(IBasketRepository basketRepository) : IQueryHandler<GetBasketQuery, GetBasketResult>
{
    public async Task<GetBasketResult> Handle(GetBasketQuery query, CancellationToken cancellationToken)
    {
        //TODO: Get Basket from Redis or Database
        var basket = await basketRepository.GetBasket(query.UserName, cancellationToken);
        return new GetBasketResult(basket);

    }
}
