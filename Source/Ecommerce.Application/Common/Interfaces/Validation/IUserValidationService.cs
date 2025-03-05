using Ecommerce.Domain.Common.ValueObjects;
using Ecommerce.Domain.UserAggregate;
using FluentResults;

namespace Ecommerce.Application.Common.Interfaces.Validation;

public interface IUserValidationService
{
    Task<Result> CheckIfUserAlreadyExistsAsync(Email email);
    Task<Result> CheckIfUserAlreadyExistsAsync(PhoneNumber phoneNumber);
}
