using FluentResults;
using MediatR;

namespace Ecommerce.Application.UseCases.Users.Commands.VerifyAccount;

public record VerifyAccountCommand(string userId, string otp) : IRequest<Result>;
