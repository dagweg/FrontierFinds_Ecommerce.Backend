using Ecommerce.Domain.Common.Models;

public sealed class Price : ValueObject
{
    public Price(decimal amount, string currency)
    {
        if (amount <= 0)
        {
            throw new ArgumentException("Price amount must be greater than zero", nameof(amount));
        }

        if (string.IsNullOrWhiteSpace(currency))
        {
            throw new ArgumentException("Currency must be provided", nameof(currency));
        }

        Amount = amount;
        Currency = currency;
    }

    public decimal Amount { get; }
    public string Currency { get; }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Amount;
        yield return Currency;
    }
}
