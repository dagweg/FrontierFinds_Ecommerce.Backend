using Ecommerce.Domain.Common.ValueObjects;

namespace Ecommerce.Domain.UserAggregate.ValueObjects;

public sealed class UserAddress : Address
{
    private UserAddress(string street, string city, string state, string country, string zipCode)
      : base(street, city, state, country, zipCode) { }

    public static new UserAddress Create(
      string street,
      string city,
      string state,
      string country,
      string zipCode
    )
    {
        return new UserAddress(street, city, state, country, zipCode);
    }
}
