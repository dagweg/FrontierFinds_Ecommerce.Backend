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
  Task<IEnumerable<TEntity>> GetAllAsync(int pageNumber, int pageSize);
  Task AddAsync(TEntity entity);
  void Update(TEntity entity);
  void Delete(TEntity entity);
}
