namespace Ecommerce.Application.UseCases.Users.Commands.RegisterUser;

using Ecommerce.Application.UseCases.Users.Common;
using FluentResults;
using MediatR;

public record RegisterUserCommand(
  string FirstName,
  string LastName,
  string Email,
  string Password,
  string ConfirmPassword,
  string PhoneNumber,
  int CountryCode
) : IRequest<Result<AuthenticationResult>>;
