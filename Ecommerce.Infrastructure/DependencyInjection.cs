namespace Ecommerce.Infrastructure;

using Ecommerce.Application.Common.Interfaces;
using Ecommerce.Application.Common.Interfaces.Authentication;
using Ecommerce.Application.Common.Interfaces.Persistence;
using Ecommerce.Infrastructure.Common;
using Ecommerce.Infrastructure.Persistence.Repositories.EfCore;
using Ecommerce.Infrastructure.Repositories.Options;
using Ecommerce.Infrastructure.Services.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Serilog;

public static class DependencyInjection
{
  // Extension method for configuring dependency injection
  public static IServiceCollection AddInfrastructure(
    this IServiceCollection services,
    IConfigurationManager configuration
  )
  {
    // Load settings from appsettings.json to appropriate classes
    services.Configure<SqlServerOptions>(configuration.GetSection(SqlServerOptions.SectionName));
    services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));

    services.AddDbContextPool<EfCoreContext>(
      (serviceProvider, options) =>
      {
        // Get the connection string from the previously configured SqlServerOptions.
        var connStr = serviceProvider
          .GetRequiredService<IOptions<SqlServerOptions>>()
          .Value.ConnectionString;

        // Connect to SqlServer using the connection string.
        options.UseSqlServer(connStr);
      }
    );

    // Register Repositories
    services.AddScoped<IUserRespository, UserRepository>();

    // Register Utility Services
    services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
    services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

    return services;
  }

  // Extension method for modifying hosting configurations
  public static IHostBuilder AddInfrastructure(this IHostBuilder hostBuilder)
  {
    // Configure Serilog
    hostBuilder.UseSerilog(
      (context, loggerConfiguration) =>
      {
        loggerConfiguration.ReadFrom.Configuration(context.Configuration);
      }
    );
    return hostBuilder;
  }
}
