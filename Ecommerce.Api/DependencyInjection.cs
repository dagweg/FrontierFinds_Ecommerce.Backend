namespace Ecommerce.Api;

using Ecommerce.Api.ActionFilters;
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

    services.Configure<ApiBehaviorOptions>(options =>
    {
      options.SuppressModelStateInvalidFilter = true;
    });

    services.AddSingleton<ProblemDetailsFactory, CustomProblemDetailsFactory>();
    return services;
  }
}
