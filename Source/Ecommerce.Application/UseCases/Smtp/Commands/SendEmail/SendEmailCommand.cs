using FluentResults;
using MediatR;

namespace Ecommerce.Application.UseCases.Stmp.Commands.SendEmail;

public class SendEmailCommand : IRequest<Result>
{
  public required string To { get; set; }
  public required string Subject { get; set; }
  public required string Body { get; set; }
}
