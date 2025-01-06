namespace Ecommerce.Domain.Common.ValueObjects;

using Ecommerce.Domain.Common.Models;

public sealed class Password : ValueObject
{
  public string Value { get; set; }

  public static Password Empty => new(string.Empty);

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
  public Password() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

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
