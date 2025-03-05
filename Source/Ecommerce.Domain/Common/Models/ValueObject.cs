namespace Ecommerce.Domain.Common.Models;

/// <summary>
/// It is the base class for all our Value Objects
/// (i.e. all objects which don't classify as Entities
/// but are part of them)
/// </summary>
public abstract class ValueObject : IEquatable<ValueObject>
{
    public bool Equals(ValueObject? other) => other is not null && Equals((object)other);

    public abstract IEnumerable<object> GetEqualityComponents();

    public override bool Equals(object? obj)
    {
        if (obj is null || obj is not ValueObject other)
        {
            return false;
        }

        return GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
    }

    public static bool operator ==(ValueObject first, ValueObject second) => first.Equals(second);

    public static bool operator !=(ValueObject first, ValueObject second) => !first.Equals(second);

    public override int GetHashCode() =>
      GetEqualityComponents().Select(x => x != null ? x.GetHashCode() : 0).Aggregate((x, y) => x ^ y);
}
