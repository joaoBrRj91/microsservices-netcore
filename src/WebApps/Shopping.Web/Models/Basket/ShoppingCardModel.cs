namespace Shopping.Web.Models.Basket;

internal class ShoppingCartModel
{
    public string UserName { get; set; } = default!;
    public List<ShoppingCartItemModel> Items { get; set; } = new();
    public decimal TotalPrice => Items.Sum(x => x.Price * x.Quantity);
}

internal class ShoppingCartItemModel
{
    public int Quantity { get; set; } = default!;
    public string Color { get; set; } = default!;
    public decimal Price { get; set; } = default!;
    public Guid ProductId { get; set; } = default!;
    public string ProductName { get; set; } = default!;
}

// wrapper classes
internal record GetBasketResponse(ShoppingCartModel Cart);
internal record StoreBasketRequest(ShoppingCartModel Cart);
internal record StoreBasketResponse(string UserName);
internal record DeleteBasketResponse(bool IsSuccess);