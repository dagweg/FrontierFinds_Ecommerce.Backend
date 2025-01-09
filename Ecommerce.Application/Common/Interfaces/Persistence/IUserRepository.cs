namespace Ecommerce.Application.Common.Interfaces.Persistence;

using Ecommerce.Domain.Common.ValueObjects;
using Ecommerce.Domain.UserAggregate;
using Ecommerce.Domain.UserAggregate.ValueObjects;

public interface IUserRepository : IRepository<User, UserId>
{
  Task<User?> GetByEmailAsync(Email email);
  Task<User?> GetByPhoneNumberAsync(PhoneNumber phoneNumber);
}
