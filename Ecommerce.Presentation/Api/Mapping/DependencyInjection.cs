namespace Ecommerce.Presentation.Api.Mapping;

public static class DependencyInjection
{
  public static IServiceCollection ConfigureAutomapper(this IServiceCollection services)
  {
    services.AddAutoMapper(
      (cfg) =>
      {
        cfg.AddMaps(typeof(Api.DependencyInjection).Assembly);
        cfg.AddMaps(typeof(Application.DependencyInjection).Assembly);
      }
    );
    return services;
  }
}
