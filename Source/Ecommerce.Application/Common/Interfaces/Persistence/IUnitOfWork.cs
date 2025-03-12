using FluentResults;

namespace Ecommerce.Application.Common.Interfaces.Persistence;

public interface IUnitOfWork
{
  Task<int> ExecuteTransactionAsync(
    Func<Task<int>> operation,
    CancellationToken cancellationToken = default
  );
  Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
