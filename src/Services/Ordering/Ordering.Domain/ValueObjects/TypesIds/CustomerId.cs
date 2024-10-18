namespace Ordering.Domain.ValueObjects.TypesIds;

public record CustomerId
{
    public Guid Value { get; init; }

    private CustomerId(Guid value) => Value = value;

    public static CustomerId Of(Guid value)
    {
        ArgumentNullException.ThrowIfNull(value);

        if(value == Guid.Empty) 
            throw new DomainException(ExceptionMessageExtension.GetEmptyMessageByNameEntity(nameof(CustomerId)));

        return new CustomerId(value);
    }
}

