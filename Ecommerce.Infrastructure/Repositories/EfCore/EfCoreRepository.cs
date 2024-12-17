namespace Ecommerce.Infrastructure.Repositories.EfCore;

using Ecommerce.Application.Common.Interfaces.Persistence;

/// <summary>
/// This class is used to define the basic CRUD operations for all ORM vendors.
/// </summary>
/// <typeparam name="TEntity">The Entity.</typeparam>
/// <typeparam name="TId"></typeparam>
public abstract class EfCoreRepository<TEntity, TId> : IRepository<TEntity, TId>
    where TEntity : class
{
    public Task AddAsync(TEntity entity) => throw new NotImplementedException();

    public Task DeleteAsync(TEntity entity) => throw new NotImplementedException();

    public Task<IEnumerable<TEntity>> GetAllAsync() => throw new NotImplementedException();

    public Task<TEntity> GetByIdAsync(TId id) => throw new NotImplementedException();

    public Task SaveChangesAsync() => throw new NotImplementedException();

    public Task UpdateAsync(TEntity entity) => throw new NotImplementedException();
}
