namespace Ecommerce.Infrastructure.Common;

public class ClientSettings
{
  public const string SectionName = "ClientSettings";

  public string ClientBaseUrl { get; set; } = null!;
}
