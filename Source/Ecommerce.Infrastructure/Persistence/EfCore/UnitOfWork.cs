using Ecommerce.Application.Common.Interfaces.Persistence;
using Ecommerce.Application.Common.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;

namespace Ecommerce.Infrastructure.Persistence.EfCore;

public class UnitOfWork : IUnitOfWork
{
  private readonly EfCoreContext _context;
  private readonly ILogger<UnitOfWork> _logger;

  public UnitOfWork(EfCoreContext context, ILogger<UnitOfWork> logger)
  {
    _context = context;
    _logger = logger;
  }

  public async Task<int> ExecuteTransactionAsync(
    Func<Task<int>> operation,
    CancellationToken cancellationToken = default
  )
  {
    var strategy = _context.Database.CreateExecutionStrategy();
    return await strategy.ExecuteAsync(async () =>
    {
      using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
      _logger.LogDebug("Transaction started.");
      try
      {
        var result = await operation(); // Execute the provided operation
        await _context.SaveChangesAsync(cancellationToken); // Save changes within the transaction
        await transaction.CommitAsync(cancellationToken);
        _logger.LogDebug("Transaction committed.");
        return result;
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, "Error during transaction. Rolling back.");
        await transaction.RollbackAsync(cancellationToken);
        _logger.LogDebug("Transaction rolled back.");
        throw; // Re-throw the exception to be caught by the caller
      }
    });
  }

  /// <summary>
  /// Save changes to the database (without transaction management here - use ExecuteTransactionAsync for transactions)
  /// </summary>
  /// <returns>
  ///   The number of state entries written to the database.
  /// </returns>
  /// <exception cref="DbUpdateException"></exception>
  public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
  {
    try
    {
      return await _context.SaveChangesAsync(cancellationToken);
    }
    catch (DbUpdateException dbEx)
    {
      // Log the exception
      _logger.LogFormattedError(dbEx, dbEx.StackTrace ?? "No stack trace available");
      throw; // Re-throw DbUpdateException
    }
    catch (Exception ex)
    {
      _logger.LogError(ex, "An unexpected error occurred while saving changes to the database.");
      throw; // Re-throw general Exception
    }
  }

  public void Dispose()
  {
    _context.Dispose();
  }

  public async ValueTask DisposeAsync()
  {
    await _context.DisposeAsync();
  }
}
