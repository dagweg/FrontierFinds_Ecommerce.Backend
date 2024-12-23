namespace Ecommerce.Domain.Common.ValueObjects;

using Ecommerce.Domain.Common.Models;

public class Email(string email) : ValueObject
{
  public string Value { get; set; } = email;
  public static Email Empty { get; } = new Email(string.Empty);

  public override IEnumerable<object> GetEqualityComponents()
  {
    yield return Value;
  }

  public static implicit operator string(Email email) => email.Value;
}
