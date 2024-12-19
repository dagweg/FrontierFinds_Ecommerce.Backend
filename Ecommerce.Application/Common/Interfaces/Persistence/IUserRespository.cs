namespace Ecommerce.Application.Common.Interfaces.Persistence;

using Ecommerce.Domain.Common.ValueObjects;
using Ecommerce.Domain.User;

public interface IUserRespository : IRepository<User, Guid>
{
    Task<User?> GetByEmailAsync(Email email);
}
