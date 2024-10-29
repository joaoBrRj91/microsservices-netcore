namespace Ordering.Domain.ValueObjects;

public record CustomerName
{
    private const int DEFAULT_TOTAL_LENGTH = 200;

    public string FirstName { get; init; } = default!;
    public string LastName { get; init; } = default!; 

    protected CustomerName() { }

    private CustomerName(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }

    public static CustomerName Of(string firstName, string lastName)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(firstName, nameof(firstName));
        ArgumentException.ThrowIfNullOrWhiteSpace(lastName, nameof(lastName));
        ArgumentOutOfRangeException.ThrowIfGreaterThan(($"{firstName}{lastName}").Length, DEFAULT_TOTAL_LENGTH);

        return new CustomerName(firstName, lastName);
    }
}
