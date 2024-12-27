namespace Ecommerce.Domain.Common.Models;

public abstract class Entity<TId>(TId id) : IEquatable<Entity<TId>>, IHasDomainEvent
{
  public TId Id { get; protected set; } = id;

  private readonly List<IDomainEvent> _domainEvents = [];

  public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

  public bool Equals(Entity<TId>? other) => Equals((object?)other);

  public abstract IEnumerable<object> GetEqualityComponents();

  public override bool Equals(object? obj)
  {
    if (obj is null || obj.GetType() != GetType())
    {
      return false;
    }

    return GetEqualityComponents().SequenceEqual(((Entity<TId>)obj).GetEqualityComponents());
  }

  public override int GetHashCode() => Id == null ? 0 : Id.GetHashCode();

  public void ClearDomainEvents()
  {
    _domainEvents.Clear();
  }

  public void AddDomainEvent(IDomainEvent domainEvent)
  {
    _domainEvents.Add(domainEvent);
  }

  public static bool operator ==(Entity<TId> left, Entity<TId> right) => left.Equals(right);

  public static bool operator !=(Entity<TId> left, Entity<TId> right) => !left.Equals(right);
}
