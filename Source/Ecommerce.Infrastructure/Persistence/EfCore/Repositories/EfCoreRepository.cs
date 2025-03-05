namespace Ecommerce.Infrastructure.Persistence.EfCore.Repositories;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Application.Common.Extensions;
using Ecommerce.Application.Common.Interfaces.Persistence;
using Ecommerce.Application.Common.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

/// <summary>
/// This repository is used to implement the basic CRUD operations for EfCore.
/// </summary>
/// <typeparam name="TEntity">The Entity.</typeparam>
/// <typeparam name="TId"></typeparam>

public abstract class EfCoreRepository<TEntity, TId>(EfCoreContext context)
  : IRepository<TEntity, TId>
  where TEntity : class
{
    public async Task<bool> AddAsync(TEntity entity)
    {
        var entityEntry = await context.AddAsync(entity);
        return entityEntry != null;
    }

    public async Task<GetAllResult<TEntity>> GetAllAsync(PaginationParameters pagination)
    {
        var query = context.Set<TEntity>().AsQueryable();

        var totalCount = query.Count();
        var entities = await query.Paginate(pagination).ToListAsync();

        return new GetAllResult<TEntity>
        {
            Items = entities,
            TotalItems = totalCount,
            TotalItemsFetched = entities.Count,
        };
    }

    public async Task<TEntity?> GetByIdAsync(TId id)
    {
        var entity = await context.FindAsync<TEntity>(id);

        return entity;
    }

    public bool Update(TEntity entity)
    {
        var updated = context.Update(entity);
        return updated != null;
    }

    public bool Delete(TEntity entity)
    {
        var deleted = context.Remove(entity);
        return deleted != null;
    }

    public async Task<bool> AnyAsync(TId id)
    {
        var entity = await context.FindAsync<TEntity>(id); // Try to find by ID
        return entity != null;
    }
}
