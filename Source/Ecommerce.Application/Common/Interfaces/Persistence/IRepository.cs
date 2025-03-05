using Ecommerce.Application.Common.Models;

namespace Ecommerce.Application.Common.Interfaces.Persistence;

/// <summary>
/// This interface is used to define the basic CRUD operations for all ORM vendors.
/// </summary>
/// <typeparam name="TEntity"></typeparam>
/// <typeparam name="TId"></typeparam>
public interface IRepository<TEntity, TId>
  where TEntity : class
{
  /// <summary>
  /// Gets an entity by its id
  /// </summary>
  /// <param name="id"></param>
  /// <returns>
  ///   the entity if its found otherwise null
  /// </returns>
  Task<TEntity?> GetByIdAsync(TId id);

  /// <summary>
  /// Gets all entities of the specified type
  /// </summary>
  /// <param name="pagination"></param>
  /// <returns>
  /// </returns>
  Task<GetAllResult<TEntity>> GetAllAsync(PaginationParameters pagination);

  /// <summary>
  /// Adds the given entity to the change tracker
  /// </summary>
  /// <param name="entity"></param>
  /// <returns>
  ///   True if adding is success otherwise false
  /// </returns>
  Task<bool> AddAsync(TEntity entity);

  /// <summary>
  /// Updates the entity with the specified entity values
  /// </summary>
  /// <param name="entity"></param>
  /// <returns>
  ///   True if update is successful otherwise false
  /// </returns>
  bool Update(TEntity entity);

  /// <summary>
  /// Deletes the entity that mathes the specified entity
  /// </summary>
  /// <param name="entity"></param>
  /// <returns>
  ///   True if delete is successful otherwise false
  /// </returns>
  bool Delete(TEntity entity);

  /// <summary>
  /// Checks if an entity with the specified id exists
  /// </summary>
  /// <param name="id"></param>
  /// <returns>
  ///   True if entity exists otherwise false
  /// </returns>
  Task<bool> AnyAsync(TId id);
}
