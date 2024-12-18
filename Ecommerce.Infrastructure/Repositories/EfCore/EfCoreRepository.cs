namespace Ecommerce.Infrastructure.Repositories.EfCore;

using Ecommerce.Application.Common.Interfaces.Logging;
using Ecommerce.Application.Common.Interfaces.Persistence;
using FluentResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.Internal;

/// <summary>
/// This class is used to implement the basic CRUD operations for EfCore.
/// </summary>
/// <typeparam name="TEntity">The Entity.</typeparam>
/// <typeparam name="TId"></typeparam>

public abstract class EfCoreRepository<TEntity, TId>(EfCoreContext context, ILogService logger)
    : IRepository<TEntity, TId>
    where TEntity : class
{
    public async Task<Result> AddAsync(TEntity entity)
    {
        if (entity is null)
        {
            return Result.Fail("Entity is null");
        }

        try
        {
            await context.AddAsync(entity);
            await SaveChangesAsync();
            return Result.Ok();
        }
        catch (Exception ex)
        {
            logger.LogError("An error occurred while adding the entity.", ex);
            return Result.Fail("An error occurred while adding the entity.");
        }
    }

    public async Task<Result> DeleteAsync(TEntity entity)
    {
        if (entity is null)
        {
            return Result.Fail("Entity is null");
        }

        try
        {
            context.Remove(entity);
            await SaveChangesAsync();
            return Result.Ok();
        }
        catch (Exception ex)
        {
            logger.LogError("An error occurred while deleting the entity.", ex);
            return Result.Fail("An error occurred while deleting the entity.");
        }
    }

    public async Task<Result<IEnumerable<TEntity>>> GetAllAsync()
    {
        try
        {
            var entities = await context.Set<TEntity>().ToListAsync();
            return Result.Ok(entities.AsEnumerable());
        }
        catch (Exception ex)
        {
            logger.LogError("An error occurred while retrieving all entities.", ex);
            return Result.Fail<IEnumerable<TEntity>>(
                "An error occurred while retrieving all entities."
            );
        }
    }

    public async Task<Result<TEntity>> GetByIdAsync(TId id)
    {
        if (id == null)
        {
            return Result.Fail<TEntity>("Id cannot be null");
        }

        try
        {
            var entity = await context.FindAsync<TEntity>(id);
            if (entity == null)
            {
                return Result.Fail<TEntity>("Entity not found");
            }

            return Result.Ok(entity);
        }
        catch (Exception ex)
        {
            logger.LogError("An error occurred while retrieving the entity by id.", ex);
            return Result.Fail<TEntity>("An error occurred while retrieving the entity by id.");
        }
    }

    public async Task<Result> UpdateAsync(TEntity entity)
    {
        if (entity is null)
        {
            return Result.Fail("Entity is null");
        }

        try
        {
            context.Update(entity);
            await SaveChangesAsync();
            return Result.Ok();
        }
        catch (Exception ex)
        {
            logger.LogError("An error occurred while updating the entity.", ex);
            return Result.Fail("An error occurred while updating the entity.");
        }
    }

    public async Task<Result> SaveChangesAsync()
    {
        try
        {
            await context.SaveChangesAsync();
            return Result.Ok();
        }
        catch (Exception ex)
        {
            logger.LogError("An error occurred while saving changes.", ex);
            throw; // Rethrow the exception to let the caller handle it
        }
    }
}
