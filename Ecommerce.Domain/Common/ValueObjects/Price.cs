namespace Ecommerce.Domain.Common.ValueObjects;

using System.Collections.Concurrent;
using Ecommerce.Domain.Common.Enums;
using Ecommerce.Domain.Common.Exceptions;
using Ecommerce.Domain.Common.Models;

public sealed class Price : ValueObject
{
  public decimal Value { get; }

  public static Price Empty => new(decimal.Zero, 0);

  public Currency Currency { get; }

  private Price() { }

  private Price(decimal value, Currency currency)
  {
    Value = value;
    Currency = currency;
  }

  public static Price Create(decimal value, Currency currency)
  {
    return new(value, currency);
  }

  public static implicit operator decimal(Price price) => price.Value;

  public static bool IsValidCurrency(string currency)
  {
    return Enum.TryParse<Currency>(currency, out _);
  }

  public static Currency ToCurrency(string currency)
  {
    if (Enum.TryParse<Currency>(currency, true, out var result))
    {
      return result;
    }

    throw new InvalidCurrencyException(currency);
  }

  public override IEnumerable<object> GetEqualityComponents()
  {
    yield return Value;
    yield return Currency;
  }
}
