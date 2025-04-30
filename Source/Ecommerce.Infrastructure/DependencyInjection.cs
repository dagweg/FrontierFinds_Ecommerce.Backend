namespace Ecommerce.Infrastructure;

using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using CloudinaryDotNet;
using Ecommerce.Application.Common;
using Ecommerce.Application.Common.Interfaces.Authentication;
using Ecommerce.Application.Common.Interfaces.Persistence;
using Ecommerce.Application.Common.Interfaces.Processors;
using Ecommerce.Application.Common.Interfaces.Providers.Date;
using Ecommerce.Application.Common.Interfaces.Providers.Forex;
using Ecommerce.Application.Common.Interfaces.Providers.Localization;
using Ecommerce.Application.Common.Interfaces.Providers.Payment.Stripe;
using Ecommerce.Application.Common.Interfaces.Providers.Search.Elastic;
using Ecommerce.Application.Common.Interfaces.Storage;
using Ecommerce.Application.Common.Utilities;
using Ecommerce.Application.Services.Storage;
using Ecommerce.Application.UseCases.Smtp.Commands.SendEmail;
using Ecommerce.Infrastructure.Common;
using Ecommerce.Infrastructure.Common.Interfaces.Providers.Forex;
using Ecommerce.Infrastructure.Common.Providers;
using Ecommerce.Infrastructure.Common.Providers.Localization;
using Ecommerce.Infrastructure.Persistence.EfCore;
using Ecommerce.Infrastructure.Persistence.EfCore.Interceptors;
using Ecommerce.Infrastructure.Persistence.EfCore.Options;
using Ecommerce.Infrastructure.Persistence.EfCore.Repositories;
using Ecommerce.Infrastructure.Services.Authentication;
using Ecommerce.Infrastructure.Services.Processors;
using Ecommerce.Infrastructure.Services.Providers.Forex;
using Ecommerce.Infrastructure.Services.Providers.Payment;
using Ecommerce.Infrastructure.Services.Providers.Payment.Stripe;
using Ecommerce.Infrastructure.Services.Providers.Search.Elastic;
using Ecommerce.Infrastructure.Services.Providers.Smtp;
using Ecommerce.Infrastructure.Services.Storage;
using Elastic.Clients.Elasticsearch;
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
using Serilog.Core;

public static class DependencyInjection
{
  static Logger Logger = new LoggerConfiguration().WriteTo.Console().CreateLogger();

  // Extension method for configuring dependency injection
  public static IServiceCollection AddInfrastructure(
    this IServiceCollection services,
    IConfigurationManager configuration
  )
  {
    // Load settings from appsettings.json
    services.AddAppSettings(configuration);

    services.AddSingleton<IElasticSearch, ElasticSearch>();

    // configure authentication and authorization
    services.AddAuth(configuration);

    services.AddInterceptors();

    services.AddSmtp(configuration);

    // register persistence (sql server, ef core, repositories)
    services.AddPersistence(configuration);

    // register localization
    services.AddLocalization();

    // register utilities (jwt token generator, date time provider etc.)
    services.AddUtilities();

    // Register Cloud Services
    services.AddCloudinary(configuration);

    // stripe, paypal .. etc
    services.AddPaymentGateways();

    return services;
  }

  public static IServiceCollection AddPersistence(
    this IServiceCollection services,
    IConfigurationManager configuration
  )
  {
    services.AddScoped<IUnitOfWork, UnitOfWork>();

    var dbOptions = configuration.GetSection(DatabaseOptions.SectionName).Get<DatabaseOptions>();

    if (dbOptions is null)
    {
      throw new InvalidOperationException("Database options are null!");
    }

    Console.WriteLine($"dbOptions.Provider value: {dbOptions.Provider}");

    if (dbOptions.Provider == DatabaseOptions.Providers.SqlServer)
    {
      services.AddDbContext<EfCoreContext>(
        (sp, options) =>
        {
          // Connect to SqlServer using the connection string.
          options
            .UseSqlServer(
              configuration[
                $"{DatabaseOptions.SectionName}:{SqlServerOptions.SectionName}:ConnectionString"
              ],
              sqlOptions =>
              {
                sqlOptions.EnableRetryOnFailure();
              }
            )
            .AddInterceptors(sp.GetRequiredService<PublishDomainEventsInterceptor>())
            .EnableDetailedErrors(true)
            .EnableSensitiveDataLogging();
        }
      );
    }
    else if (dbOptions.Provider == DatabaseOptions.Providers.PgSql)
    {
      services.AddDbContext<EfCoreContext>(
        (sp, options) =>
        {
          // Connect to PgSql using the connection string.
          options
            .UseNpgsql(
              configuration[
                $"{DatabaseOptions.SectionName}:{PgSqlOptions.SectionName}:ConnectionString"
              ]
            )
            .AddInterceptors(sp.GetRequiredService<PublishDomainEventsInterceptor>())
            .EnableDetailedErrors(true)
            .EnableSensitiveDataLogging();
        }
      );
    }
    else
    {
      throw new InvalidOperationException("Database provider is not supported!");
    }

    // Register Repositories
    services.AddScoped<IUserRepository, UserRepository>();
    services.AddScoped<IProductRepository, ProductRepository>();
    services.AddScoped<IProductReviewRepository, ProductReviewRepository>();
    services.AddScoped<IOrderRepository, OrderRepository>();

    return services;
  }

  // Extension method for modifying hosting configurations
  public static IHostBuilder AddHostConfigurations(this IHostBuilder hostBuilder)
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
            if (
              context.HttpContext.Request.Cookies.TryGetValue(
                cookieSettings.CookieKey,
                out var token
              ) && !string.IsNullOrEmpty(token)
            )
            {
              context.Token = token;
            }

            LogPretty.Log(context.HttpContext.Request.Cookies);

            return Task.CompletedTask;
          },
        };
      });
    return services;
  }

  public static IServiceCollection AddInterceptors(this IServiceCollection services)
  {
    services.AddScoped<PublishDomainEventsInterceptor>();
    services.AddScoped<UpdateAuditableEntitiesInterceptor>();
    return services;
  }

  public static IServiceCollection AddAppSettings(
    this IServiceCollection services,
    IConfigurationManager configuration
  )
  {
    services.Configure<ElasticSettings>(configuration.GetSection(ElasticSettings.SectionName));

    services.Configure<DatabaseOptions>(configuration.GetSection(DatabaseOptions.SectionName));

    services.Configure<PaymentOptions>(configuration.GetSection(PaymentOptions.SectionName));

    services.Configure<ClientSettings>(configuration.GetSection(ClientSettings.SectionName));

    services.Configure<CookieSettings>(configuration.GetSection(CookieSettings.SectionName));
    services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));
    services.Configure<CloudinarySettings>(
      configuration.GetSection(CloudinarySettings.SectionName)
    );
    return services;
  }

  public static IServiceCollection AddUtilities(this IServiceCollection services)
  {
    services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
    services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
    services.AddSingleton<IImageProcessor, ImageProcessor>();

    services.AddSingleton<IExchangeApiClient, ExchangeApiClient>();
    services.AddSingleton<IForexSerivce, ForexService>();
    return services;
  }

  public static IServiceCollection AddCloudinary(
    this IServiceCollection services,
    IConfigurationManager configuration
  )
  {
    services.AddScoped<ICloudinary>(p =>
    {
      var cloudinarySettings = p.GetRequiredService<IOptions<CloudinarySettings>>().Value;
      return new Cloudinary(cloudinarySettings.GetUrl);
    });

    services.AddScoped<ICloudinaryService, CloudinaryService>();
    services.AddScoped<ICloudStorageService, CloudinaryService>();

    services.AddScoped<ICloudinaryResourceTracker, CloudinaryResourceTracker>();
    services.AddScoped<IExternalResourceTracker, CloudinaryResourceTracker>();
    return services;
  }

  public static IServiceCollection AddSmtp(
    this IServiceCollection services,
    IConfigurationManager configuration
  )
  {
    services.AddScoped<ISmtpClientWrapper>(sp =>
    {
      string? host = configuration[$"{EmailSettings.SectionName}:Host"];

      if (host is null)
      {
        Log.Warning("Smtp host is not configured. Email sending will be disabled.");
      }

      return new SmtpClientWrapper(host ?? "smtp.gmail.com");
    });
    return services;
  }

  public static IServiceCollection AddPaymentGateways(this IServiceCollection services)
  {
    services.AddTransient<IStripeService, StripeService>();
    return services;
  }
}
