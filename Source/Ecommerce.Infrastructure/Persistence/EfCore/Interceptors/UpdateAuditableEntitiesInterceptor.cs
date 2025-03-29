using Ecommerce.Domain.Common.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Ecommerce.Infrastructure.Persistence.EfCore.Interceptors;

public class UpdateAuditableEntitiesInterceptor : SaveChangesInterceptor
{
  public override InterceptionResult<int> SavingChanges(
    DbContextEventData eventData,
    InterceptionResult<int> result
  )
  {
    UpdateEntities(eventData.Context);
    return base.SavingChanges(eventData, result);
  }

  public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(
    DbContextEventData eventData,
    InterceptionResult<int> result,
    CancellationToken cancellationToken = default
  )
  {
    UpdateEntities(eventData.Context);
    return await base.SavingChangesAsync(eventData, result, cancellationToken);
  }

  private void UpdateEntities(DbContext? context)
  {
    if (context == null)
      return;

    var entries = context
      .ChangeTracker.Entries()
      .Where(e =>
        e.Entity is Entity<object>
        && ( // Use Entity<object> to match any TId
          e.State == EntityState.Added || e.State == EntityState.Modified
        )
      );

    foreach (var entityEntry in entries)
    {
      // Access the properties through the cast to Entity<object>
      var entity = (Entity<object>)entityEntry.Entity; // Safe to cast since the Where clause filters by Entity<object>
      entity.UpdatedAt = DateTime.UtcNow;
    }
  }
}
