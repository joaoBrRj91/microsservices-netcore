using Ordering.Application.Dtos;
using Ordering.Domain.Enums;
using Ordering.Domain.Models;
using Ordering.Domain.ValueObjects;

namespace Ordering.Application.Builders;

internal static class OrderToOrderDtoEntityBuilder
{
    public static OrderDto BuildOrderDto(this Order order)
    {
        return new OrderDto(
                Id: order.Id.Value,
                CustomerId: order.CustomerId.Value,
                OrderName: order.OrderName.Value,
                ShippingAddress: BuildAddressDto(order.ShippingAddress),
                BillingAddress: BuildAddressDto(order.BillingAddress),
                Payment: BuildPaymentDto(order.Payment),
                Status: (int)order.OrderStatus,
                OrderItems: BuildOrderItemsDto(order.OrderItems)
            );
    }

    private static AddressDto BuildAddressDto(Address address)
    {
        return new AddressDto(
            FirstName: address.CustomerName.FirstName,
            LastName: address.CustomerName.LastName,
            EmailAddress: address.EmailAddress,
            AddressLine: address.AddressLine,
            Country: address.Country,
            State: address.State,
            ZipCode: address.ZipCode
            );
    }

    private static PaymentDto BuildPaymentDto(Payment payment)
    {
        if (!Enum.IsDefined(typeof(PaymentMethod), payment.PaymentMethod))
            throw new ArgumentException($"Payment Method {payment.PaymentMethod} " +
                $"is not valid in PaymentMethod Type : {string.Join(';', Enum.GetNames<PaymentMethod>())}");

        return new PaymentDto(
            payment.CardName,
            payment.CardNumber,
            payment.Expiration,
            payment.CVV,
            (int)payment.PaymentMethod
            );
    }


    private static IEnumerable<OrderItemDto> BuildOrderItemsDto(IEnumerable<OrderItem> orderItems)
    {
        foreach (var orderItem in orderItems)
        {
            yield return new OrderItemDto(
                orderItem.OrderId.Value,
                orderItem.ProductId.Value,
                orderItem.Quantity,
                orderItem.Price);
        }
    }
}
