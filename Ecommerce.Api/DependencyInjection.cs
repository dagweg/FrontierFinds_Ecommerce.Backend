namespace Ecommerce.Api;

using Ecommerce.Api.ActionFilters;
using Ecommerce.Api.Services;
using Ecommerce.Application.Common.Interfaces.Providers.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

public static class DependencyInjection
{
  public static IServiceCollection AddApi(this IServiceCollection services)
  {
    services.AddControllers(options =>
    {
      // Register global action filters
      options.Filters.Add<FluentResultsActionFilter>();
      options.Filters.Add<ModelBindingErrorActionFilter>();
    });

    services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

    services.Configure<ApiBehaviorOptions>(options =>
    {
      options.SuppressModelStateInvalidFilter = true;
    });

    services.AddSingleton<ProblemDetailsFactory, CustomProblemDetailsFactory>();

    services.AddScoped<IUserContextService, UserContextService>();

    return services;
  }
}
