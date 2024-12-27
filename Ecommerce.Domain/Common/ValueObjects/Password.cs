namespace Ecommerce.Domain.Common.ValueObjects;

using Ecommerce.Domain.Common.Models;

public sealed class Password : ValueObject
{
  public string Value { get; set; } = Empty;

  public static Password Empty => new(string.Empty);

  public Password() { }

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
