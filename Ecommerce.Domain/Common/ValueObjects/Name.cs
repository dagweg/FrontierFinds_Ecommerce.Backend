namespace Ecommerce.Domain.Common.ValueObjects;

using Ecommerce.Domain.Common.Models;

public class Name(string name) : ValueObject
{
  public string Value { get; set; } = name;
  public static Name Empty { get; } = new Name(string.Empty);

  public static implicit operator string(Name name) => name.Value;

  public override IEnumerable<object> GetEqualityComponents()
  {
    yield return Value;
  }
}
