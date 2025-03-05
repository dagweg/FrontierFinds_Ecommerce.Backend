using Ecommerce.Domain.Common.Models;

namespace Ecommerce.Domain.UserAggregate.ValueObjects;

public sealed class CartId : ValueObject
{
    public Guid Value { get; }

    public static CartId Empty => new(Guid.Empty);

    private CartId() { }

    private CartId(Guid value)
    {
        Value = value;
    }

    public static CartId CreateUnique() => new(Guid.NewGuid());

    public static CartId Convert(Guid value) => new(value);

    public static implicit operator string(CartId cartId) => cartId.Value.ToString();

    public static implicit operator Guid(CartId cartId) => cartId.Value;

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
