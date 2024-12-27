namespace Ecommerce.Domain.Common.ValueObjects;

using Ecommerce.Domain.Common.Models;

public sealed class Email : ValueObject
{
  public string Value { get; set; } = Empty;

  public static Email Empty => new(string.Empty);

  private Email() { }

  private Email(string email)
  {
    Value = email;
  }

  public static Email Create(string email) => new(email);

  public override IEnumerable<object> GetEqualityComponents()
  {
    yield return Value;
  }

  public static implicit operator string(Email email) => email.Value;
}
