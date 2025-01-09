namespace Ecommerce.Infrastructure;

using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Ecommerce.Application.Common;
using Ecommerce.Application.Common.Interfaces.Authentication;
using Ecommerce.Application.Common.Interfaces.Persistence;
using Ecommerce.Application.Common.Interfaces.Providers.Date;
using Ecommerce.Application.Common.Interfaces.Providers.Localization;
using Ecommerce.Infrastructure.Common.Providers;
using Ecommerce.Infrastructure.Common.Providers.Localization;
using Ecommerce.Infrastructure.Persistence.EfCore;
using Ecommerce.Infrastructure.Persistence.EfCore.Interceptors;
using Ecommerce.Infrastructure.Persistence.EfCore.Options;
using Ecommerce.Infrastructure.Persistence.EfCore.Repositories;
using Ecommerce.Infrastructure.Services.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Serilog;

public static class DependencyInjection
{
  // Extension method for configuring dependency injection
  public static IServiceCollection AddInfrastructure(
    this IServiceCollection services,
    IConfigurationManager configuration
  )
  {
    services.AddAuth(configuration);

    services.AddScoped<PublishDomainEventsInterceptor>();

    services.AddPersistence(configuration);

    services.AddLocalization();

    // Load settings from appsettings.json to appropriate classes
    services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));

    // Register Utility Services
    services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
    services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

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
          .UseSqlServer(configuration[$"{SqlServerOptions.SectionName}:ConnectionString"])
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

  public static IServiceCollection AddLocalization(this IServiceCollection services)
  {
    services.AddScoped<IValidationMessages>(_ => new ValidationMessageProvider(
      "Ecommerce.Application.Common.Resources.ValidationMessages", // path to the resource (.resx) file
      ApplicationAssembly.Assembly
    ));

    services.AddScoped<IAuthenticationMessages>(_ => new AuthenticationMessageProvider(
      "Ecommerce.Application.Common.Resources.AuthenticationMessages",
      ApplicationAssembly.Assembly
    ));

    return services;
  }

  public static IServiceCollection AddAuth(
    this IServiceCollection services,
    IConfigurationManager configuration
  )
  {
    services
      .AddAuthentication("Bearer")
      .AddJwtBearer(options =>
      {
        options.TokenValidationParameters = new TokenValidationParameters
        {
          ValidateIssuer = true,
          ValidateAudience = true,
          ValidateLifetime = true,
          ValidateIssuerSigningKey = true,
          ValidIssuer = configuration[$"{JwtSettings.SectionName}:{nameof(JwtSettings.Issuer)}"],
          ValidAudience = configuration[
            $"{JwtSettings.SectionName}:{nameof(JwtSettings.Audience)}"
          ],
          IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(
              configuration[$"{JwtSettings.SectionName}:{nameof(JwtSettings.SecretKey)}"]
                ?? throw new InvalidOperationException("Jwt Secret key is null!")
            )
          ),
        };
      });
    return services;
  }
}
