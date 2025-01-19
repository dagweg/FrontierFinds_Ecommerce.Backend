namespace Ecommerce.Application;

using Ecommerce.Application.Behaviors;
using Ecommerce.Application.Common;
using Ecommerce.Application.Common.Interfaces.Validation;
using Ecommerce.Application.Services.Validation;
using Ecommerce.Application.UseCases.Images.Commands;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
  public static IServiceCollection AddApplication(this IServiceCollection services)
  {
    // MediatR Registration
    services.AddMediatR(ApplicationAssembly.Assembly);

    // Register MediatR Pipeline Behaviors
    services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

    // Register All Fluent Validation Models
    services.AddValidatorsFromAssembly(ApplicationAssembly.Assembly);

    services.AddTransient<IValidator<CreateImageCommand>, CreateImageCommandValidator>();

    services.AddScoped<IUserValidationService, UserValidationService>();

    return services;
  }
}
