using System.ComponentModel.DataAnnotations;
using FluentResults;
using MediatR;

namespace Ecommerce.Application.UseCases.Users.Commands.ResendEmailOtp;

public record ResendEmailOtpCommand(string userId) : IRequest<Result<ResendEmailOtpResult>>;
