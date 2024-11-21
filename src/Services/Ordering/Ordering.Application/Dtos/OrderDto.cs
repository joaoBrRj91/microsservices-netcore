﻿namespace Ordering.Application.Dtos;

public record OrderDto(
    Guid Id,
    Guid CustomerId,
    string OrderName,
    AddressDto ShippingAddress, 
    AddressDto BillingAddress,
    PaymentDto Payment, 
    string Status, 
    IEnumerable<OrderItemDto> OrderItems);

