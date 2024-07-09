namespace Basket.API.Models;

public class ShoppingCart
{
    public string UserName { get; set; } = default!;
    public List<ShoppingCardItem> Items { get; set; } = [];
    public decimal TotalPrice => Items.Sum(i => i.Price * i.Quantity);

    //For Mapping
    public ShoppingCart() { }
    
    public ShoppingCart(string userName) => UserName = userName;
    
}
