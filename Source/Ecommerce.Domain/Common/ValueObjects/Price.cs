namespace Ecommerce.Domain.Common.ValueObjects;

using System.Collections.Concurrent;
using Ecommerce.Domain.Common.Enums;
using Ecommerce.Domain.Common.Errors;
using Ecommerce.Domain.Common.Models;
using FluentResults;

public sealed class Price : ValueObject
{
  public long ValueInCents { get; }

  public static Price Empty => new(0);

  public const Currency BASE_CURRENCY = Currency.USD;

  private Price() { }

  private Price(long valueInCents)
  {
    ValueInCents = valueInCents;
  }

  /// <summary>
  /// Create a new Price object. Make sure the value is in the <see cref="BASE_CURRENCY"/>.
  /// </summary>
  /// <param name="valueInBaseCurrency"></param>
  /// <returns></returns>
  public static Price CreateInBaseCurrency(long valueInBaseCurrencyCents)
  {
    return new(valueInBaseCurrencyCents);
  }

  public static implicit operator decimal(Price price) => price.ValueInCents;

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
    yield return ValueInCents;
  }
}
