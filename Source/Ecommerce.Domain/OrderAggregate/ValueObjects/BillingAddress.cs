using Ecommerce.Domain.Common.ValueObjects;

namespace Ecommerce.Domain.OrderAggregate.ValueObjects;

public sealed class BillingAddress : Address
{
    private BillingAddress(string street, string city, string state, string country, string zipCode)
      : base(street, city, state, country, zipCode) { }

    public static new BillingAddress Create(
      string street,
      string city,
      string state,
      string country,
      string zipCode
    )
    {
        return new BillingAddress(street, city, state, country, zipCode);
    }
}
