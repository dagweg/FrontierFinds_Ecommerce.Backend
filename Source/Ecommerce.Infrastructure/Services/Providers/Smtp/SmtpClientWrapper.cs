using System.Net.Mail;

namespace Ecommerce.Infrastructure.Services.Providers.Smtp;

public class SmtpClientWrapper : SmtpClient, ISmtpClientWrapper
{
  public SmtpClientWrapper(string host)
    : base(host) { }

  public new virtual string? Host
  {
    get { return base.Host; }
    set { base.Host = value ?? ""; }
  }

  public new virtual event SendCompletedEventHandler SendCompleted
  {
    add => base.SendCompleted += value;
    remove => base.SendCompleted -= value;
  }

  public new virtual void SendAsync(MailMessage message, object? userToken)
  {
    base.SendAsync(message, userToken);
  }
}
