using Ecommerce.Application.Common.Models.Persistence;
using FluentResults;

namespace Ecommerce.Application.Common.Interfaces.Persistence;

public interface IUnitOfWork
{
  IEnumerable<ChangeTrackerEntryInfo> GetChangeTrackerEntries();

  Task<int> ExecuteTransactionAsync(
    Func<Task<int>> operation,
    CancellationToken cancellationToken = default
  );
  Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
