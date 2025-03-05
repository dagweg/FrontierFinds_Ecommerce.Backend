using Ecommerce.Domain.Common.Enums;
using Ecommerce.Domain.Common.Models;
using Ecommerce.Domain.Common.ValueObjects;

namespace Ecommerce.Domain.OrderAggregate.ValueObjects;

public sealed class OrderTotal : ValueObject
{
    public decimal Value { get; set; }
    public Currency Currency { get; set; }

    public static OrderTotal Empty => new(0, Price.BASE_CURRENCY);

    private OrderTotal(decimal value, Currency currency)
    {
        Value = value;
        Currency = currency;
    }

    public static OrderTotal Create(decimal value, Currency currency)
    {
        return new(value, currency);
    }

    public static implicit operator decimal(OrderTotal total) => total.Value;

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    private OrderTotal() { }
}
