using Ordering.Application.Dtos;
using Ordering.Domain.Enums;
using Ordering.Domain.Models;
using Ordering.Domain.ValueObjects;
using Ordering.Domain.ValueObjects.TypesIds;

namespace Ordering.Application.Builders;

internal static class OrderDtoToOrderEntityBuilder
{
    public static Order BuildCreateOrder(this OrderDto orderDto)
    {
        var order = Order.Create(
            id: OrderId.Of(Guid.NewGuid()),
            customerId: CustomerId.Of(orderDto.CustomerId),
            orderName: OrderName.Of(orderDto.OrderName),
            shippingAddress: BuildAddress(orderDto.ShippingAddress),
            billingAddress: BuildAddress(orderDto.BillingAddress),
            payment: BuildPayment(orderDto.Payment));

        return order;
    }

    public static void BuildUpdateOrder(this OrderDto orderDto, Order order)
    {
        order.Update(
            shippingAddress: BuildAddress(orderDto.ShippingAddress),
            billingAddress: BuildAddress(orderDto.BillingAddress),
            payment: BuildPayment(orderDto.Payment),
            orderStatus: (OrderStatus)orderDto.Status
            );
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
        if (!Enum.IsDefined(typeof(PaymentMethod), paymentDto.PaymentMethod))
            throw new ArgumentException($"Payment Method {paymentDto.PaymentMethod} " +
                $"is not valid in PaymentMethod Type : {string.Join(';', Enum.GetNames<PaymentMethod>())}");

        return Payment.Of(paymentDto.CardName,
            paymentDto.CardNumber,
            paymentDto.Expiration,
            paymentDto.Cvv,
            (PaymentMethod)paymentDto.PaymentMethod);
    }
}
