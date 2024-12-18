using FluentResults;

namespace Ecommerce.Application.Common.Interfaces.Persistence;

/// <summary>
/// This interface is used to define the basic CRUD operations for all ORM vendors.
/// </summary>
/// <typeparam name="TEntity"></typeparam>
/// <typeparam name="TId"></typeparam>

public interface IRepository<TEntity, TId>
    where TEntity : class
{
    Task<Result<TEntity>> GetByIdAsync(TId id);
    Task<Result<IEnumerable<TEntity>>> GetAllAsync();
    Task<Result> AddAsync(TEntity entity);
    Task<Result> UpdateAsync(TEntity entity);
    Task<Result> DeleteAsync(TEntity entity);
    Task<Result> SaveChangesAsync();
}
