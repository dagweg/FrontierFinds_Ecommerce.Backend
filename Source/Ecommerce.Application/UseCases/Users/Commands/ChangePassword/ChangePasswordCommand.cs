using FluentResults;
using MediatR;

namespace Ecommerce.Application.UseCases.Users.Commands.ChangePassword;

public class ChangePasswordCommand : IRequest<Result>
{
    public required string CurrentPassword { get; set; }
    public required string NewPassword { get; set; }
}
