using Ecommerce.Domain.Common.Models;

namespace Ecommerce.Domain.Common.ValueObjects;

public sealed class PhoneNumber : ValueObject
{
  public string Value { get; set; }
  public static PhoneNumber Empty => new(string.Empty);

  private PhoneNumber(string value)
  {
    Value = value;
  }

  public static PhoneNumber Create(string value)
  {
    return new(value);
  }

  public static implicit operator string(PhoneNumber phoneNumber) => phoneNumber.Value;

  public override IEnumerable<object> GetEqualityComponents()
  {
    yield return Value;
  }
}
