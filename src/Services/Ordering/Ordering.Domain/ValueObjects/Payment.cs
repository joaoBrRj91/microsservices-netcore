namespace Ordering.Domain.ValueObjects;

public record Payment
{
    public string CardName { get; init; }
    public string CardNumber { get; init; }
    public string Expiration { get; init; }
    public string CVV { get; init; }
    public PaymentMethod PaymentMethod { get; init; }

    protected Payment() { }

    private Payment(string cardName, string cardNumber, string expiration, string cvv, PaymentMethod paymentMethod)
    {
        CardName = cardName;
        CardNumber = cardNumber;
        Expiration = expiration;
        CVV = cvv;
        PaymentMethod = paymentMethod;
    }


    public static Payment Of(string cardName, string cardNumber, string expiration, string cvv, PaymentMethod paymentMethod)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(cardName);
        ArgumentException.ThrowIfNullOrWhiteSpace(cardNumber);
        ArgumentException.ThrowIfNullOrWhiteSpace(expiration);
        ArgumentOutOfRangeException.ThrowIfEqual((int)paymentMethod, (int)PaymentMethod.None);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(cvv.Length, 3);

        return new Payment(cardName, cardNumber, expiration, cvv, paymentMethod);
    }
}