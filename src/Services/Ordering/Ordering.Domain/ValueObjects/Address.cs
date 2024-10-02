namespace Ordering.Domain.ValueObjects;

public record Address
{
    public CustomerName CustomerName { get; init; }
    public string EmailAddress { get; init; }
    public string AddressLine { get; init; }
    public string Country { get; init; }
    public string State { get; init; }
    public string ZipCode { get; init; }


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
