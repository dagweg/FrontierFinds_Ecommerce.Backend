using Ecommerce.Application.Common.Interfaces.Persistence;
using Ecommerce.Application.Common.Utilities;
using Ecommerce.Domain.Common.Exceptions;
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

  public async Task<int> SaveChangesAsync()
  {
    try
    {
      return await _context.SaveChangesAsync();
    }
    catch (DbUpdateException dbEx)
    {
      // Log the exception
      // You can inspect dbEx to see if it's a constraint violation or something specific
      _logger.LogFormattedError(dbEx, dbEx.StackTrace ?? "No stack trace available");
      throw new DbUpdateException("An error occurred while saving changes.", dbEx);
    }
  }
}
