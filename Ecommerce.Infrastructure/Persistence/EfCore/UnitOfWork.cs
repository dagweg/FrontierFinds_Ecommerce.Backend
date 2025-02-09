using Ecommerce.Application.Common.Interfaces.Persistence;
using Ecommerce.Application.Common.Utilities;
using Microsoft.EntityFrameworkCore;
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

  /// <summary>
  /// Save changes to the database
  /// </summary>
  /// <returns>
  ///   The number of state entries written to the database. Otherwise null if an error occurred while saving changes
  /// </returns>
  /// <exception cref="DbUpdateException"></exception>
  public async Task<int?> SaveChangesAsync(CancellationToken cancellationToken = default)
  {
    try
    {
      return await _context.SaveChangesAsync(cancellationToken);
    }
    catch (DbUpdateException dbEx)
    {
      // Log the exception
      // You can inspect dbEx to see if it's a constraint violation or something specific
      _logger.LogFormattedError(dbEx, dbEx.StackTrace ?? "No stack trace available");
      return null;
    }
  }
}
