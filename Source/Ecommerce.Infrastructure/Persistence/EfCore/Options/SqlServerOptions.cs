namespace Ecommerce.Infrastructure.Persistence.EfCore.Options;

/// <summary>
/// Represents the options for the SQL Server.
/// </summary>

public class SqlServerOptions
{
  public const string SectionName = "SqlServerSettings";
  public string ConnectionString { get; set; } = string.Empty;
}
