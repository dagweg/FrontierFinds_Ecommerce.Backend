namespace Ecommerce.Domain.UserAggregate.ValueObjects;

using Ecommerce.Domain.Common.Models;

public sealed class Name : ValueObject
{
  public string Value { get; set; } = Empty;

  public static Name Empty => new(string.Empty);

  private Name() { }

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
