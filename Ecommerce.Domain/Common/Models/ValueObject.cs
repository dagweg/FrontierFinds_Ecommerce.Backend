public abstract class ValueObject<T> : IEquatable<ValueObject<T>>
{
    public T Value { get; }

    protected ValueObject(T value) => Value = value;

    public abstract IEnumerable<object> GetEqualityComponents();

    public override bool Equals(object? obj)
    {
        if (obj is null || obj is not ValueObject<T> other) return false;

        return GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
    }

    public static bool operator ==(ValueObject<T> first, ValueObject<T> second) => first.Equals(second);
    public static bool operator !=(ValueObject<T> first, ValueObject<T> second) => !first.Equals(second);


    public override int GetHashCode()
    {
        return GetEqualityComponents()
            .Select(x => x != null ? x.GetHashCode() : 0)
            .Aggregate((x, y) => x ^ y);
    }

    public bool Equals(ValueObject<T>? other) => other is not null && Equals((object)other);
}