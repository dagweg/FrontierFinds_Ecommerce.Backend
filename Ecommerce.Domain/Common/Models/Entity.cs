public abstract class Entity<TId> : IEquatable<Entity<TId>>
{

    public TId Id { get; protected set; }

    protected Entity(TId id)
    {
        Id = id;
    }

    public abstract IEnumerable<object> GetEqualityComponents();

    public override bool Equals(object? obj)
    {
        if (obj is null || obj.GetType() != GetType())
            return false;

        return GetEqualityComponents().SequenceEqual(((Entity<TId>)obj).GetEqualityComponents());
    }

    public override int GetHashCode()
    {
        return Id == null ? 0 : Id.GetHashCode();
    }

    public static bool operator ==(Entity<TId> left, Entity<TId> right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(Entity<TId> left, Entity<TId> right)
    {
        return !left.Equals(right);
    }

    public bool Equals(Entity<TId>? other)
    {
        return Equals((object?)other);
    }
}