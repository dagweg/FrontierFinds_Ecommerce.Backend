namespace Ecommerce.Application.Common.Interfaces.Persistence;

/// <summary>
/// This interface is used to define the basic CRUD operations for all ORM vendors.
/// </summary>
/// <typeparam name="TEntity"></typeparam>
/// <typeparam name="TId"></typeparam>

public interface IRepository<TEntity, TId>
  where TEntity : class
{
  Task<TEntity?> GetByIdAsync(TId id);
  Task<IEnumerable<TEntity>> GetAllAsync();
  Task AddAsync(TEntity entity);
  Task UpdateAsync(TEntity entity);
  Task DeleteAsync(TEntity entity);
  Task SaveChangesAsync();
}
