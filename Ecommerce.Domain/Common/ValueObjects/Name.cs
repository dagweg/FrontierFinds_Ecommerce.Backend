namespace Ecommerce.Domain.Common.ValueObjects;

using Ecommerce.Domain.Common.Models;

public class Name : ValueObject
{
  public string Value { get; set; }
  public static Name Empty => new(string.Empty);

  private Name(string name)
  {
    Value = name;
  }

  public static Name Create(string name) => new(name);

  public static implicit operator string(Name name) => name.Value;

  public override IEnumerable<object> GetEqualityComponents()
  {
    yield return Value;
  }
}
