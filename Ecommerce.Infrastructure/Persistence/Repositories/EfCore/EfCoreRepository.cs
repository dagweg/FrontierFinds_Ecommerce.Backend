namespace Ecommerce.Infrastructure.Persistence.Repositories.EfCore;

using Ecommerce.Application.Common.Interfaces.Persistence;
using Microsoft.EntityFrameworkCore;

/// <summary>
/// This class is used to implement the basic CRUD operations for EfCore.
/// </summary>
/// <typeparam name="TEntity">The Entity.</typeparam>
/// <typeparam name="TId"></typeparam>

public abstract class EfCoreRepository<TEntity, TId>(EfCoreContext context)
  : IRepository<TEntity, TId>
  where TEntity : class
{
  public async Task AddAsync(TEntity entity)
  {
    await context.AddAsync(entity);
    await SaveChangesAsync();
  }

  public async Task DeleteAsync(TEntity entity)
  {
    context.Remove(entity);
    await SaveChangesAsync();
  }

  public async Task<IEnumerable<TEntity>> GetAllAsync()
  {
    var entities = await context.Set<TEntity>().ToListAsync();
    return entities.AsEnumerable();
  }

  public async Task<TEntity?> GetByIdAsync(TId id)
  {
    var entity = await context.FindAsync<TEntity>(id);
    return entity;
  }

  public async Task UpdateAsync(TEntity entity)
  {
    context.Update(entity);
    await SaveChangesAsync();
  }

  public async Task SaveChangesAsync()
  {
    await context.SaveChangesAsync();
  }
}
