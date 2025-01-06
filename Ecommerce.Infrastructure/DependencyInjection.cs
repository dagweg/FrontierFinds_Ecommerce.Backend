namespace Ecommerce.Infrastructure;

using Ecommerce.Application.Common.Interfaces;
using Ecommerce.Application.Common.Interfaces.Authentication;
using Ecommerce.Application.Common.Interfaces.Persistence;
using Ecommerce.Infrastructure.Common;
using Ecommerce.Infrastructure.Persistence.EfCore;
using Ecommerce.Infrastructure.Persistence.EfCore.Interceptors;
using Ecommerce.Infrastructure.Persistence.EfCore.Options;
using Ecommerce.Infrastructure.Persistence.EfCore.Repositories;
using Ecommerce.Infrastructure.Services.Authentication;
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
    services.AddScoped<PublishDomainEventsInterceptor>();

    services.AddPersistence(configuration);

    // Load settings from appsettings.json to appropriate classes
    services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));

    // Register Utility Services
    services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
    services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
    services.AddScoped<IValidationMessageProvider, ValidationMessageProvider>();

    return services;
  }

  public static IServiceCollection AddPersistence(
    this IServiceCollection services,
    IConfigurationManager configuration
  )
  {
    // load in sql server configurations
    services.Configure<SqlServerOptions>(configuration.GetSection(SqlServerOptions.SectionName));

    services.AddDbContext<EfCoreContext>(
      (sp, options) =>
      {
        // Connect to SqlServer using the connection string.
        options
          .UseSqlServer(
            "Server=EVOO-EG-LP7\\SQLEXPRESS;Database=ecommerce;Trusted_Connection=True;TrustServerCertificate=True;"
          )
          .AddInterceptors(sp.GetRequiredService<PublishDomainEventsInterceptor>())
          .EnableDetailedErrors(true);
      }
    );

    // Register Repositories
    services.AddScoped<IUserRepository, UserRepository>();
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
