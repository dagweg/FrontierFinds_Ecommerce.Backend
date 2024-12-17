namespace Ecommerce.Application.UseCases.UserManagement.Authentication.Commands;

using Ecommerce.Application.UseCases.UserManagement.Authentication.Common;
using FluentResults;
using MediatR;

public record RegisterUserCommand(
    string FirstName,
    string LastName,
    string Email,
    string Password,
    string PhoneNumber,
    int CountryCode
) : IRequest<Result<AuthenticationResult>>;
