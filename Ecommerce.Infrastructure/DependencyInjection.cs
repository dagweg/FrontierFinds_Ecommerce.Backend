namespace Ecommerce.Infrastructure;

using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using Ecommerce.Application.Common;
using Ecommerce.Application.Common.Interfaces.Authentication;
using Ecommerce.Application.Common.Interfaces.Persistence;
using Ecommerce.Application.Common.Interfaces.Providers.Date;
using Ecommerce.Application.Common.Interfaces.Providers.Localization;
using Ecommerce.Infrastructure.Common;
using Ecommerce.Infrastructure.Common.Providers;
using Ecommerce.Infrastructure.Common.Providers.Localization;
using Ecommerce.Infrastructure.Persistence.EfCore;
using Ecommerce.Infrastructure.Persistence.EfCore.Interceptors;
using Ecommerce.Infrastructure.Persistence.EfCore.Options;
using Ecommerce.Infrastructure.Persistence.EfCore.Repositories;
using Ecommerce.Infrastructure.Services.Authentication;
using IdentityModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
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
    services.Configure<CookieSettings>(configuration.GetSection(CookieSettings.SectionName));

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

    services.AddScoped<IUnitOfWork, UnitOfWork>();

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
    services.AddScoped<IProductRepository, ProductRepository>();

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
    var cookieSettings =
      configuration.GetSection(CookieSettings.SectionName).Get<CookieSettings>()
      ?? throw new InvalidOperationException("Cookie settings are null!");

    var jwtSettings =
      configuration.GetSection(JwtSettings.SectionName).Get<JwtSettings>()
      ?? throw new InvalidOperationException("Jwt settings are null!");

    services
      .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
      .AddJwtBearer(options =>
      {
        options.TokenValidationParameters = new TokenValidationParameters
        {
          ValidateIssuer = true,
          ValidateAudience = true,
          ValidateLifetime = true,
          ValidateIssuerSigningKey = true,
          ValidIssuer = jwtSettings.Issuer,
          ValidAudience = jwtSettings.Audience,
          IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(
              jwtSettings.SecretKey
                ?? throw new InvalidOperationException("Jwt Secret key is null!")
            )
          ),
        };

        options.Events = new JwtBearerEvents
        {
          OnMessageReceived = context =>
          {
            context.HttpContext.Request.Cookies.TryGetValue(
              cookieSettings.CookieKey,
              out var token
            );
            if (!string.IsNullOrEmpty(token))
            {
              context.Token = token;
            }

            return Task.CompletedTask;
          },
        };
      });
    return services;
  }
}
