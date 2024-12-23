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

    public static IEnumerable<OrderDto> BuildOrdersDto(this List<Order> orders)
    {
        var ordersDto = new List<OrderDto>(orders.Count);
        ordersDto.AddRange(orders.Select(order => BuildOrderDto(order)));
        return ordersDto;
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


    #region Order Item
    private static OrderItemDto BuildOrderItemDto(this OrderItem orderItem) => new(
            orderItem.OrderId.Value,
            orderItem.ProductId.Value,
            orderItem.Quantity,
            orderItem.Price);

    private static IEnumerable<OrderItemDto> BuildOrderItemsDto(IEnumerable<OrderItem> orderItems)
    {
        var orderItemsDto = new List<OrderItemDto>(orderItems.Count());
        orderItemsDto.AddRange(orderItems.Select(orderItem => BuildOrderItemDto(orderItem)));
        return orderItemsDto;
    }
    #endregion

}
