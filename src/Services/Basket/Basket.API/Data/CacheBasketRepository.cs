
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Basket.API.Data;

public class CacheBasketRepository(IBasketRepository basketRepository, IDistributedCache cache) 
    : IBasketRepository
{

    public async Task<ShoppingCart> GetBasket(string userName, CancellationToken cancellationToken = default)
    {
        var cachedBasket = await cache.GetStringAsync(userName, cancellationToken);

        if (!string.IsNullOrWhiteSpace(cachedBasket))
            return JsonSerializer.Deserialize<ShoppingCart>(cachedBasket)!;

        return await basketRepository.GetBasket(userName, cancellationToken);  
    }

    public async Task StoreBasket(ShoppingCart cart, CancellationToken cancellationToken = default)
    {
        await basketRepository.StoreBasket(cart, cancellationToken);

        await cache.SetStringAsync(cart.UserName, JsonSerializer.Serialize(cart));
    }

    public  async Task DeleteBasket(string userName, CancellationToken cancellationToken = default)
    {
        await basketRepository.DeleteBasket(userName, cancellationToken);

        await cache.RemoveAsync(userName, cancellationToken);
    }
}
