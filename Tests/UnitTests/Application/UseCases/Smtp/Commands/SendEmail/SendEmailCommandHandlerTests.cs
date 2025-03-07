using System.ComponentModel;
using System.Net;
using System.Net.Mail;
using Ecommerce.Application.UseCases.Smtp.Commands.SendEmail;
using Ecommerce.Application.UseCases.Stmp.Commands.SendEmail;
using Ecommerce.Infrastructure.Services.Providers.Smtp;
using FluentResults;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace Ecommerce.Application.UnitTests.UseCases.Stmp.Commands;

public class SendEmailCommandHandlerTests
{
  private readonly Mock<IOptions<EmailSettings>> _emailSettingsMock;
  private readonly Mock<ILogger<SendEmailCommandHandler>> _loggerMock;
  private readonly Mock<ISmtpClientWrapper> _smtpClientMock;
  private readonly EmailSettings _testEmailSettings;
  private readonly SendEmailCommandHandler _sendEmailCommandHandler;

  public SendEmailCommandHandlerTests()
  {
    _emailSettingsMock = new Mock<IOptions<EmailSettings>>();
    _loggerMock = new Mock<ILogger<SendEmailCommandHandler>>();
    _smtpClientMock = new Mock<ISmtpClientWrapper>();
    _testEmailSettings = new EmailSettings()
    {
      Address = "test@example.com",
      DisplayName = "Test Sender",
      Host = "smtp.example.com",
      Port = 587,
      UserName = "smtpuser",
      Password = "smtppassword",
      EnableSsl = true,
    };
    _emailSettingsMock.Setup(s => s.Value).Returns(_testEmailSettings);

    _sendEmailCommandHandler = new SendEmailCommandHandler(
      _emailSettingsMock.Object,
      _loggerMock.Object,
      _smtpClientMock.Object
    );

    _smtpClientMock.SetupProperty(s => s.Port, _testEmailSettings.Port);
    _smtpClientMock.SetupProperty(s => s.EnableSsl, _testEmailSettings.EnableSsl);
    _smtpClientMock.SetupProperty(
      s => s.Credentials,
      new NetworkCredential(_testEmailSettings.UserName, _testEmailSettings.Password)
    );
  }

  [Fact]
  public async Task Handle_SuccessfulEmailSend_ReturnsSuccess()
  {
    // Arrange
    var command = new SendEmailCommand
    {
      To = "recipient@example.com",
      Subject = "Test Subject",
      Body = "Test Body",
    };

    _smtpClientMock
      .Setup(x => x.SendAsync(It.IsAny<MailMessage>(), It.IsAny<object>()))
      .Callback<MailMessage, object>(
        (message, userToken) =>
        {
          _smtpClientMock.Raise(
            s => s.SendCompleted += null,
            new AsyncCompletedEventArgs(null, false, null)
          );
        }
      );

    // Act
    var result = await _sendEmailCommandHandler.Handle(command, CancellationToken.None);

    // Assert
    Assert.True(result.IsSuccess);
  }

  [Fact]
  public async Task Handle_CancelledEmailSend_ReturnsFailure()
  {
    // Arrange
    var command = new SendEmailCommand
    {
      To = "recipient@example.com",
      Subject = "Test Subject",
      Body = "Test Body",
    };

    _smtpClientMock
      .Setup(x => x.SendAsync(It.IsAny<MailMessage>(), It.IsAny<object>()))
      .Callback<MailMessage, object>(
        (message, userToken) =>
        {
          _smtpClientMock.Raise(
            s => s.SendCompleted += null,
            new AsyncCompletedEventArgs(new SmtpException(), true, null)
          );
        }
      );

    // Act
    var result = await _sendEmailCommandHandler.Handle(command, CancellationToken.None);

    // Assert
    Assert.True(result.IsFailed);
    Assert.Contains("Email sending was cancelled", result.Errors.Select(e => e.Message));
  }

  [Fact]
  public async Task Handle_FailedEmailSend_ReturnsFailureAndLogsError()
  {
    // Arrange
    var command = new SendEmailCommand
    {
      To = "recipient@example.com",
      Subject = "Test Subject",
      Body = "Test Body",
    };

    // Mock SmtpClient and simulate error

    _smtpClientMock
      .Setup(x => x.SendAsync(It.IsAny<MailMessage>(), It.IsAny<object>()))
      .Callback<MailMessage, object>(
        (message, userToken) =>
        {
          _smtpClientMock.Raise(
            s => s.SendCompleted += null,
            new AsyncCompletedEventArgs(new SmtpException(), false, null)
          );
        }
      );

    // Act
    var result = await _sendEmailCommandHandler.Handle(command, CancellationToken.None);

    // Assert
    Assert.True(result.IsFailed);
    Assert.Contains("Email sending failed", result.Errors.Select(e => e.Message));
  }
}
