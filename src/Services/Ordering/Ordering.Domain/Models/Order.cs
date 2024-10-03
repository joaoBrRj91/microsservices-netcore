using Ordering.Domain.ValueObjects.TypesIds;

namespace Ordering.Domain.Models;

public class Order : Aggregate<OrderId>
{
    public CustomerId CustomerId { get; private set; }
    public OrderName OrderName { get; private set; }
    public decimal TotalPrice { get; private set; }
    public Address ShippingAddress { get; private set; }
    public Address BillingAddress { get; private set; }
    public Payment Payment { get; private set; }
    public OrderStatus OrderStatus { get; private set; }

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

        //Add domain event in list of events for dispacher after trigger save changes in database

        return order;
    }

    public void Update(OrderName orderName, Address shippingAddress, Address billingAddress, Payment payment, OrderStatus orderStatus)
    {
        OrderName = orderName;
        ShippingAddress = shippingAddress;
        BillingAddress = billingAddress;
        Payment = payment;
        OrderStatus = orderStatus;

        //Add domain event in list of events for dispacher after trigger save changes in database
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

        //throw domain exception if not find order item
    }

    public void GenereteTotalPriceOrder() => TotalPrice = _orderItems.Sum(o => o.Price * o.Quantity);

}
