using Ordering.Application.Dtos;
using Ordering.Domain.Enums;
using Ordering.Domain.Models;
using Ordering.Domain.ValueObjects;
using Ordering.Domain.ValueObjects.TypesIds;

namespace Ordering.Application.Builders.CreateOrder;

internal static class OrderDtoToCreateOrderEntityBuilder
{
    public static Order BuildOrder(this OrderDto orderDto)
    {
        var order = Order.Create(
            id: OrderId.Of(Guid.NewGuid()),
            customerId: CustomerId.Of(orderDto.CustomerId),
            orderName: OrderName.Of(orderDto.OrderName),
            shippingAddress: BuildAddress(orderDto.ShippingAddress),
            billingAddress: BuildAddress(orderDto.BillingAddress),
            payment: BuildPayment(orderDto.Payment));

        order.BuildOrderItems(orderDto.OrderItems);

        return order;
    }

    private static Address BuildAddress(AddressDto addressDto)
    {
        return Address.Of(addressDto.FirstName,
            addressDto.LastName,
            addressDto.EmailAddress,
            addressDto.AddressLine,
            addressDto.Country,
            addressDto.State,
            addressDto.ZipCode);
    }

    private static Payment BuildPayment(PaymentDto paymentDto)
    {
        Enum.TryParse(paymentDto.PaymentMethod, true, out PaymentMethod paymentMethod);

        return Payment.Of(paymentDto.CardName, paymentDto.CardNumber, paymentDto.Expiration, paymentDto.Cvv, paymentMethod);
    }

    private static void BuildOrderItems(this Order order, IEnumerable<OrderItemDto> orderItemsDto)
    {
        foreach (var orderItemDto in orderItemsDto)
        {
            order.AddOrderItem(ProductId.Of(orderItemDto.ProductId), orderItemDto.Quantity, orderItemDto.Price);
        }
    }
}
