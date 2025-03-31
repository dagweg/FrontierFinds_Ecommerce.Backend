using Ecommerce.Domain.Common.Enums;
using Ecommerce.Domain.Common.Models;
using Ecommerce.Domain.Common.ValueObjects;

namespace Ecommerce.Domain.OrderAggregate.ValueObjects;

public sealed class OrderTotal : ValueObject
{
  public long ValueTotalInCents { get; set; }
  public Currency Currency { get; set; }

  public static OrderTotal Empty => new(0, Price.BASE_CURRENCY);

  private OrderTotal(long valueTotalInCents, Currency currency)
  {
    ValueTotalInCents = valueTotalInCents;
    Currency = currency;
  }

  public static OrderTotal Create(long valueTotalInCents, Currency currency)
  {
    return new(valueTotalInCents, currency);
  }

  public static implicit operator decimal(OrderTotal total) => total.ValueTotalInCents;

  public override IEnumerable<object> GetEqualityComponents()
  {
    yield return ValueTotalInCents;
    yield return Currency;
  }

  private OrderTotal() { }
}
