using Ordering.Domain.ValueObjects.TypesIds;

namespace Ordering.Domain.Models;

public class OrderItem : Entity<OrderItemId>
{
    internal OrderItem(OrderId orderId, ProductId productId, decimal price, ushort quantity)
    {
        OrderId = orderId;
        ProductId = productId;
        Price = price;
        Quantity = quantity;
    }

    public OrderId OrderId { get; private set; }
    public ProductId ProductId { get; private set; }
    public decimal Price { get; private set; }
    public ushort Quantity { get; private set; }

}
