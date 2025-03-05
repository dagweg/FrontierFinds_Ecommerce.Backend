using System.Net;
using System.Net.Mail;

public interface ISmtpClientWrapper
{
  string? Host { get; set; }
  int Port { get; set; }
  ICredentialsByHost? Credentials { get; set; }
  bool EnableSsl { get; set; }

  event SendCompletedEventHandler SendCompleted;
  void SendAsync(MailMessage message, object? userToken);
}
