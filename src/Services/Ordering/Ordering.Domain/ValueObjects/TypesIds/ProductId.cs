namespace Ordering.Domain.ValueObjects.TypesIds;

public record ProductId
{
    public Guid Value { get; init; }

    private ProductId(Guid value) => Value = value;

    public static ProductId Of(Guid value)
    {
        ArgumentNullException.ThrowIfNull(value);

        if (value == Guid.Empty)
            throw new DomainException(ExceptionMessageExtension.GetEmptyMessageByNameEntity(nameof(ProductId)));

        return new ProductId(value);
    }
}

