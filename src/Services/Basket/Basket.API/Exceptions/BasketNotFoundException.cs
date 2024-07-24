using BuildingBlocks.Core.Exceptions;

namespace Basket.API.Exceptions;

public class BasketNotFoundException(string userName) : NotFoundException(nameof(ShoppingCart), userName);

