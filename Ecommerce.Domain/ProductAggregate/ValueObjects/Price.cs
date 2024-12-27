namespace Ecommerce.Domain.ProductAggregate.ValueObjects;

using Ecommerce.Domain.Common.Enums;
using Ecommerce.Domain.Common.Models;

public sealed class Price : ValueObject
{
  public decimal Value { get; } = decimal.Zero;

  public Currency Currency { get; } = Currency.NONE;

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

  public override IEnumerable<object> GetEqualityComponents()
  {
    yield return Value;
    yield return Currency;
  }
}
