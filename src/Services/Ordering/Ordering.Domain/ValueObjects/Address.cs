namespace Ordering.Domain.ValueObjects;

public record Address
{
    //Mapping this complex type
    public CustomerName CustomerName { get; init; } = default!;
    public string EmailAddress { get; init; } = default!;
    public string AddressLine { get; init; } = default!;
    public string Country { get; init; }    = default!;
    public string State { get; init; } = default!;
    public string ZipCode { get; init; } = default!;


    protected Address() { }

    private Address(
        CustomerName customerName, 
        string emailAddress,
        string addressLine,
        string country, 
        string state, 
        string zipCode)
    {
        CustomerName = customerName;
        EmailAddress = emailAddress;
        AddressLine = addressLine;
        Country = country;
        State = state;
        ZipCode = zipCode;
    }

    
    public static Address Of(
        string customerFirstName,
        string customerLastName,
        string emailAddress,
        string addressLine,
        string country,
        string state,
        string zipCode)
    {

        ArgumentException.ThrowIfNullOrWhiteSpace(emailAddress);
        ArgumentException.ThrowIfNullOrWhiteSpace(addressLine);

        var customerName = CustomerName.Of(customerFirstName, customerLastName);

        return new Address(customerName, emailAddress, addressLine, country, state, zipCode);
    }
}
