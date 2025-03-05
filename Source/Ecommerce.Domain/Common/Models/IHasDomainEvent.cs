namespace Ecommerce.Domain.Common.Models;

/// <summary>
/// Enforces domain event specific behaviors and properties
/// to our entities. Used for event handling inside of database
/// interceptors. (i.e. for filtering entities which have domain events)
/// </summary>
public interface IHasDomainEvent
{
  IReadOnlyList<IDomainEvent> DomainEvents { get; }

  void ClearDomainEvents();
  void AddDomainEvent(IDomainEvent domainEvent);
}
