namespace Ecommerce.Domain.Common.Models;

/// <summary>
/// The base class for all our aggregate entry points (roots).
/// </summary>
/// <typeparam name="TId"></typeparam>
/// <param name="id"></param>
public abstract class AggregateRoot<TId>(TId id) : Entity<TId>(id) { }
