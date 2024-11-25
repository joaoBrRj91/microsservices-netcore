namespace Ordering.Domain.ValueObjects;

public record Payment
{
    public string CardName { get; init; } = default!;
    public string CardNumber { get; init; } = default!;
    public string Expiration { get; init; } = default!;
    public string CVV { get; init; } = default!;
    public PaymentMethod PaymentMethod { get; init; } = PaymentMethod.None;

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
        if (paymentMethod == PaymentMethod.None)
            throw new InvalidOperationException(nameof(paymentMethod));

        ArgumentException.ThrowIfNullOrWhiteSpace(cardName);
        ArgumentException.ThrowIfNullOrWhiteSpace(cardNumber);
        ArgumentException.ThrowIfNullOrWhiteSpace(expiration);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(cvv.Length, 3);

        return new Payment(cardName, cardNumber, expiration, cvv, paymentMethod);
    }
}