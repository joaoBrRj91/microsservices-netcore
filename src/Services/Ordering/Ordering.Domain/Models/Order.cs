using Ordering.Domain.ValueObjects.TypesIds;

namespace Ordering.Domain.Models;

public class Order : Aggregate<OrderId>
{
    public CustomerId CustomerId { get; private set; } = default!;
    public OrderName OrderName { get; private set; } = default!;
    public decimal TotalPrice { get; private set; } 
    public Address ShippingAddress { get; private set; } = default!;
    public Address BillingAddress { get; private set; } = default!;
    public Payment Payment { get; private set; } = default!;
    public OrderStatus OrderStatus { get; private set; } = OrderStatus.Draft;

    private readonly List<OrderItem> _orderItems = [];

    //Throw exception if user try force ToList()
    public IReadOnlyCollection<OrderItem> OrderItems => _orderItems.AsReadOnly();


    public static Order Create(OrderId id, CustomerId customerId, OrderName orderName, Address shippingAddress, Address billingAddress, Payment payment)
    {
        var order = new Order
        {
            Id = id,
            CustomerId = customerId,
            OrderName = orderName,
            ShippingAddress = shippingAddress,
            BillingAddress = billingAddress,
            Payment = payment,
            OrderStatus = OrderStatus.Pending
        };

        order.AddDomainEvent(new OrderCreateEvent(order));
        return order;
    }

    public void Update(OrderName orderName, Address shippingAddress, Address billingAddress, Payment payment, OrderStatus orderStatus)
    {
        OrderName = orderName;
        ShippingAddress = shippingAddress;
        BillingAddress = billingAddress;
        Payment = payment;
        OrderStatus = orderStatus;

        AddDomainEvent(new OrderUpdateEvent(this));
    }


    public void AddOrderItem(ProductId productId, int quantity, decimal price)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(quantity);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(price);

        var orderItem = new OrderItem(Id, productId, price, quantity);
        _orderItems.Add(orderItem);
    }

    public void RemoveOrderItem(ProductId productId)
    {
        var orderItem = _orderItems.FirstOrDefault(o => o.ProductId == productId);
        if(orderItem is not null) _orderItems.Remove(orderItem);

        throw new DomainException($"Product {productId.Value} is not found");
    }

    public void GenereteTotalPriceOrder() => TotalPrice = _orderItems.Sum(o => o.Price * o.Quantity);

}
