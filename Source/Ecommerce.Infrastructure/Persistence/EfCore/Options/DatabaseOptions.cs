namespace Ecommerce.Infrastructure.Persistence.EfCore.Options;

public class DatabaseOptions
{
  public const string SectionName = "DatabaseSettings";

  public required string Provider { get; set; }

  public static class Providers
  {
    public const string SqlServer = "sqlserver";
    public const string PgSql = "postgresql";
  }

  public required PgSqlOptions PgSqlOptions { get; set; }
  public required SqlServerOptions SqlServerOptions { get; set; }
}
