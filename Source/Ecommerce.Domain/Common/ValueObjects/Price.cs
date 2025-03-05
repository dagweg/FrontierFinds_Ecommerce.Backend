namespace Ecommerce.Domain.Common.ValueObjects;

using System.Collections.Concurrent;
using Ecommerce.Domain.Common.Enums;
using Ecommerce.Domain.Common.Errors;
using Ecommerce.Domain.Common.Models;
using FluentResults;

public sealed class Price : ValueObject
{
    public decimal Value { get; }

    public static Price Empty => new(decimal.Zero);

    public const Currency BASE_CURRENCY = Currency.ETB;

    private Price() { }

    private Price(decimal value)
    {
        Value = value;
    }

    /// <summary>
    /// Create a new Price object. Make sure the value is in the <see cref="BASE_CURRENCY"/>.
    /// </summary>
    /// <param name="valueInBaseCurrency"></param>
    /// <returns></returns>
    public static Price CreateInBaseCurrency(decimal valueInBaseCurrency)
    {
        return new(valueInBaseCurrency);
    }

    public static implicit operator decimal(Price price) => price.Value;

    public static bool IsValidCurrency(string currency)
    {
        return Enum.TryParse<Currency>(currency, out _);
    }

    public static Result<Currency> ToCurrency(string currency)
    {
        if (Enum.TryParse<Currency>(currency, true, out var result))
        {
            return Result.Ok(result);
        }

        return InvalidCurrencyError.GetResult(nameof(currency), "Invalid Currency", currency);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
