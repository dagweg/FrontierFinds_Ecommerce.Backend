namespace Ecommerce.Domain.Common.ValueObjects;

using Ecommerce.Domain.Common.Models;

public sealed class UserId : ValueObject
{
  private UserId(Guid value)
  {
    Value = value;
  }

  public Guid Value { get; }

  public static Guid Empty => Guid.Empty;

  public static UserId CreateUnique() => new(Guid.NewGuid());

  public override IEnumerable<object> GetEqualityComponents()
  {
    yield return Value;
  }

  public static implicit operator string(UserId userId)
  {
    return userId.Value.ToString();
  }
}
