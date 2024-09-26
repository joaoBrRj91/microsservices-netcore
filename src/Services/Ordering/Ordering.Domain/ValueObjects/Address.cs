namespace Ordering.Domain.ValueObjects;

public record Address(
    CustomerName CustomerName,
    string EmailAddress,
    string AddressLine,
    string Country,
    string State,
    string ZipCode);
