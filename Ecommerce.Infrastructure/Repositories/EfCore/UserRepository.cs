namespace Ecommerce.Infrastructure.Repositories.EfCore;

using Ecommerce.Application.Common.Interfaces.Persistence;
using Ecommerce.Domain.Common.ValueObjects;
using Ecommerce.Domain.User;

public class UserRepository : EfCoreRepository<User, Guid>, IUserRespository
{
    public Task<User> GetByEmailAsync(Email email) => throw new NotImplementedException();
}
