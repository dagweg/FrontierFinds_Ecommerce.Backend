using Ecommerce.Domain.Common.Models;

namespace Ecommerce.Domain.Common.ValueObjects;

public class Address : ValueObject
{
  public string Street { get; private set; } = string.Empty;
  public string City { get; private set; } = string.Empty;
  public string State { get; private set; } = string.Empty;
  public string Country { get; private set; } = string.Empty;
  public string ZipCode { get; private set; } = string.Empty;

  public override IEnumerable<object> GetEqualityComponents()
  {
    yield return Street;
    yield return City;
    yield return State;
    yield return Country;
    yield return ZipCode;
  }
}
