namespace Ecommerce.Presentation.Api;

using Microsoft.AspNetCore.Mvc.Infrastructure;

public static class DependencyInjection
{
  public static IServiceCollection AddApi(this IServiceCollection services)
  {
    services.AddControllers();

    services.AddSingleton<ProblemDetailsFactory, CustomProblemDetailsFactory>();
    return services;
  }
}
