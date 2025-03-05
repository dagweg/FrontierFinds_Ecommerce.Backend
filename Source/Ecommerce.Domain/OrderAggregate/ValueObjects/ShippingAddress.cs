using Ecommerce.Domain.Common.ValueObjects;

namespace Ecommerce.Domain.OrderAggregate.ValueObjects;

public sealed class ShippingAddress : Address
{
    private ShippingAddress(string street, string city, string state, string country, string zipCode)
      : base(street, city, state, country, zipCode) { }

    public static new ShippingAddress Create(
      string street,
      string city,
      string state,
      string country,
      string zipCode
    )
    {
        return new ShippingAddress(street, city, state, country, zipCode);
    }
}
