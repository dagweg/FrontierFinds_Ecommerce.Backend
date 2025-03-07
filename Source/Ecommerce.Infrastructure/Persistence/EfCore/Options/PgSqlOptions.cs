namespace Ecommerce.Infrastructure.Persistence.EfCore.Options;

public class PgSqlOptions
{
  public const string SectionName = "PgSqlSettings";
  public string ConnectionString { get; set; } = string.Empty;
}
