namespace Ecommerce.Application.Common.Interfaces.Persistence;

using Ecommerce.Domain.Common.ValueObjects;
using Ecommerce.Domain.User;

public interface IUserRepository : IRepository<User, UserId>
{
  Task<User?> GetByEmailAsync(Email email);
}
