namespace Ecommerce.Domain.Common.Models;

public abstract class AggregateRoot<TId>(TId id) : Entity<TId>(id) { }
