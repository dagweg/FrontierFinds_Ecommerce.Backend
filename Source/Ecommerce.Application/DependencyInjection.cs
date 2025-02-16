namespace Ecommerce.Application;

using Ecommerce.Application.Behaviors.Pipelines;
using Ecommerce.Application.Common;
using Ecommerce.Application.Common.Interfaces.Storage;
using Ecommerce.Application.Common.Interfaces.Validation;
using Ecommerce.Application.Services.Validation;
using Ecommerce.Application.UseCases.Images.CreateImage;
using Ecommerce.Application.UseCases.Smtp.Commands.SendEmail;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
  public static IServiceCollection AddApplication(
    this IServiceCollection services,
    IConfiguration configuration
  )
  {
    // Load EmailSettings Configuration from appsettings.json
    services.Configure<EmailSettings>(configuration.GetSection(EmailSettings.SectionName));

    services.AddMediatR(ApplicationAssembly.Assembly);

    services.AddMediatRBehaviors();

    // Register All Fluent Validation Models
    services.AddValidatorsFromAssembly(ApplicationAssembly.Assembly);

    services.AddScoped<IUserValidationService, UserValidationService>();
    services.AddScoped<IProductValidationService, ProductValidationService>();

    return services;
  }

  private static IServiceCollection AddMediatRBehaviors(this IServiceCollection services)
  {
    // Register MediatR Pipeline Behaviors
    services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
    services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CompensationBehavior<,>));

    return services;
  }
}
