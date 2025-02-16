namespace Ecommerce.Application.UseCases.Smtp.Commands.SendEmail;

public class EmailSettings
{
  public const string SectionName = nameof(EmailSettings);

  public required string Host { get; set; }
  public required string DisplayName { get; set; }
  public required string UserName { get; set; }
  public required string Address { get; set; }
  public required string Password { get; set; }
  public required int Port { get; set; }
  public required bool EnableSsl { get; set; }
}
