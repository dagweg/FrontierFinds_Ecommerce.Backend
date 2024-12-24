namespace Ecommerce.Domain.Common.ValueObjects;

using Ecommerce.Domain.Common.Models;

public class Password : ValueObject
{
  public string Value { get; set; }
  public static Password Empty => new(string.Empty);

  private Password(string password)
  {
    Value = password;
  }

  public static Password Create(string password) => new(password);

  public static implicit operator string(Password password) => password.Value;

  public override IEnumerable<object> GetEqualityComponents()
  {
    yield return Value;
  }
}
