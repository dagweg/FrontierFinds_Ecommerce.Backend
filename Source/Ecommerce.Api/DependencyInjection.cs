namespace Ecommerce.Api;

using Ecommerce.Api.ActionFilters;
using Ecommerce.Api.Exceptions;
using Ecommerce.Api.Exceptions.Handlers;
using Ecommerce.Api.Services;
using Ecommerce.Application.Common.Interfaces.Providers.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

public static class DependencyInjection
{
  public static IServiceCollection AddApi(this IServiceCollection services)
  {
    services.AddExceptionHandlers();

    services.AddControllers(options =>
    {
      // Register global action filters
      options.Filters.Add<FluentResultsActionFilter>();
      options.Filters.Add<ModelBindingErrorActionFilter>();
    });

    services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
    services.AddTransient(typeof(IPasswordHasher<>), typeof(PasswordHasher<>));

    services.Configure<ApiBehaviorOptions>(options =>
    {
      options.SuppressModelStateInvalidFilter = true;
    });

    services.AddScoped<IUserContextService, UserContextService>();

    return services;
  }

  public static IServiceCollection AddExceptionHandlers(this IServiceCollection services)
  {
    services.AddSingleton<ExceptionHandlerFactory>();
    services.AddSingleton<KeyNotFoundExceptionHandler>();
    services.AddSingleton<UnauthorizedAccessExceptionHandler>();
    services.AddSingleton<GenericExceptionHandler>();

    return services;
  }
}
