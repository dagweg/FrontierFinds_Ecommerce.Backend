using Ecommerce.Application.Common.Interfaces.Persistence;

namespace Ecommerce.Infrastructure.Persistence.EfCore;

public class UnitOfWork : IUnitOfWork
{
  private readonly EfCoreContext _context;

  public UnitOfWork(EfCoreContext context)
  {
    _context = context;
  }

  public async Task<int> SaveChangesAsync()
  {
    return await _context.SaveChangesAsync();
  }
}
