namespace Ecommerce.Infrastructure;

using Ecommerce.Application.Common.Interfaces.Logging;
using Ecommerce.Infrastructure.Logging;
using Ecommerce.Infrastructure.Repositories.EfCore;
using Ecommerce.Infrastructure.Repositories.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfigurationManager configuration
    )
    {
        // Options Pattern for SqlServerOptions
        services.Configure<SqlServerOptions>(
            configuration.GetSection(SqlServerOptions.SectionName)
        );

        services.AddDbContextPool<EfCoreContext>(
            (serviceProvider, options) =>
            {
                // Get the connection string from the previously configured SqlServerOptions.
                var connStr = serviceProvider
                    .GetRequiredService<SqlServerOptions>()
                    .ConnectionString;

                // Connect to SqlServer using the connection string.
                options.UseSqlServer(connStr);
            }
        );

        // Register Serilog logging service.
        services.AddSingleton<ILogService, SerilogService>();

        return services;
    }
}
