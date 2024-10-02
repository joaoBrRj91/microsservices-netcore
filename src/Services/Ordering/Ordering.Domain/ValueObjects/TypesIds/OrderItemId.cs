namespace Ordering.Domain.ValueObjects.TypesIds;

public record OrderItemId
{
    public Guid Guid { get; init; }

    private OrderItemId(Guid value) => Guid = value;

    public static OrderItemId Of(Guid value)
    {
        ArgumentNullException.ThrowIfNull(value);

        if (value == Guid.Empty)
            throw new DomainException(ExceptionMessageExtension.GetEmptyMessageByNameEntity(nameof(OrderItemId)));

        return new OrderItemId(value);
    }
}


