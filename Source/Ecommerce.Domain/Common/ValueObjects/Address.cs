using Ecommerce.Domain.Common.Models;

namespace Ecommerce.Domain.Common.ValueObjects;

public class Address : ValueObject
{
  public string Street { get; set; } = string.Empty;
  public string City { get; set; } = string.Empty;
  public string State { get; set; } = string.Empty;
  public string Country { get; set; } = string.Empty;
  public string ZipCode { get; set; } = string.Empty;

  protected Address() { }

  protected Address(string street, string city, string state, string country, string zipCode)
  {
    Street = street;
    City = city;
    State = state;
    Country = country;
    ZipCode = zipCode;
  }

  public static Address Create(
    string street,
    string city,
    string state,
    string country,
    string zipCode
  )
  {
    return new Address(street, city, state, country, zipCode);
  }

  public override IEnumerable<object> GetEqualityComponents()
  {
    yield return Street;
    yield return City;
    yield return State;
    yield return Country;
    yield return ZipCode;
  }
}
