namespace Ecommerce.Application.Common.Interfaces.Persistence;

public interface IUnitOfWork
{
  Task<int> SaveChangesAsync();
}
