namespace Ordering.Domain.Models;

public class Order : Aggregate<Guid>
{
    public Guid CustomerId { get; private set; }
    public string OrderName { get; private set; }
    public decimal TotalPrice { get; private set; }
    public Address ShippingAddress { get; private set; }
    public Address BillingAddress { get; private set; }
    public Payment Payment { get; private set; }
    public OrderStatus OrderStatus { get; private set; }

    private readonly List<OrderItem> _orderItems = [];
    public IReadOnlyList<OrderItem> OrderItems => _orderItems.AsReadOnly();

    public void GenereteTotalPriceOrder() => TotalPrice = _orderItems.Sum(o => o.Price * o.Quantity);
}
