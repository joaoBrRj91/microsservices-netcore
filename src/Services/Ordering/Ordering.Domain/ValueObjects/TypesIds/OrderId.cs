﻿namespace Ordering.Domain.ValueObjects.TypesIds;

public record OrderId
{
    public Guid Guid { get; init; }

    private OrderId(Guid value) => Guid = value;

    public static OrderId Of(Guid value)
    {
        ArgumentNullException.ThrowIfNull(value);

        if (value == Guid.Empty)
            throw new DomainException(ExceptionMessageExtension.GetEmptyMessageByNameEntity(nameof(OrderId)));


        return new OrderId(value);
    }
}


