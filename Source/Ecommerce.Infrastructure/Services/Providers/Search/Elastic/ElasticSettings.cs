namespace Ecommerce.Infrastructure.Services.Providers.Search.Elastic;

public class ElasticSettings
{
  public const string SectionName = "ElasticSettings";
  public string ConnectionString { get; set; } = null!;
  public string Username { get; set; } = null!;
  public string Password { get; set; } = null!;
  public string DefaultIndex { get; set; } = null!;
}
