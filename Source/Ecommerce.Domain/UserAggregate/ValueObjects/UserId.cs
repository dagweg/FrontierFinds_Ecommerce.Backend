namespace Ecommerce.Domain.UserAggregate.ValueObjects;

using Ecommerce.Domain.Common.Models;

public sealed class UserId : ValueObject
{
    public Guid Value { get; }

    public static UserId Empty => new(Guid.Empty);

    private UserId() { }

    private UserId(Guid value)
    {
        Value = value;
    }

    public static UserId CreateUnique() => new(Guid.NewGuid());

    public static UserId Convert(Guid value) => new(value);

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public static implicit operator string(UserId userId) => userId.Value.ToString();

    public static implicit operator Guid(UserId userId) => userId.Value;
}
