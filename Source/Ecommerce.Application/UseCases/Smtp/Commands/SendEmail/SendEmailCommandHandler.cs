using System.ComponentModel;
using System.Net;
using System.Net.Mail;
using System.Security.Authentication;
using Ecommerce.Application.UseCases.Smtp.Commands.SendEmail;
using FluentResults;
using MediatR;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Ecommerce.Application.UseCases.Stmp.Commands.SendEmail;

public class SendEmailCommandHandler(
  IOptions<EmailSettings> emailSettings,
  ILogger<SendEmailCommandHandler> logger
) : IRequestHandler<SendEmailCommand, Result>
{
  private readonly EmailSettings _emailSettings = emailSettings.Value;

  public async Task<Result> Handle(SendEmailCommand command, CancellationToken cancellationToken)
  {
    MailMessage mail = new MailMessage();
    mail.From = new MailAddress(_emailSettings.Address, _emailSettings.DisplayName);
    mail.To.Add(command.To);
    mail.Subject = command.Subject;
    mail.Body = command.Body;
    mail.IsBodyHtml = true;

    SmtpClient smtp = new SmtpClient(_emailSettings.Host);
    smtp.Port = _emailSettings.Port;
    smtp.Credentials = new NetworkCredential(_emailSettings.UserName, _emailSettings.Password);
    smtp.EnableSsl = _emailSettings.EnableSsl;

    var tcs = new TaskCompletionSource<Result>();

    smtp.SendCompleted += (sender, e) =>
    {
      if (e.Cancelled)
      {
        logger.LogWarning("Email sending was cancelled");
        tcs.SetResult(Result.Fail("Email sending was cancelled"));
      }
      else if (e.Error != null)
      {
        logger.LogError(e.Error, "Email sending failed: {Message}", e.Error.Message);
        tcs.SetResult(Result.Fail("Email sending failed"));
      }
      else
      {
        logger.LogInformation(
          "Email sent to {To} with subject {Subject}",
          command.To,
          command.Subject
        );
        tcs.SetResult(Result.Ok());
      }
    };

    smtp.SendAsync(mail, null);

    return await tcs.Task;
  }
}
