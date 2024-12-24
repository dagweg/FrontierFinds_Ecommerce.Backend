namespace Ecommerce.Presentation.Api.Mapping;

public static class DependencyInjection
{
  public static IServiceCollection ConfigureAutomapper(this IServiceCollection services)
  {
    services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    return services;
  }
}
