/*
  Entry Point of our program
*/

namespace Api;

using Ecommerce.Api;
using Ecommerce.Api.Mapping;
using Ecommerce.Api.Middlewares;
using Ecommerce.Application;
using Ecommerce.Application.UseCases.Images.Common;
using Ecommerce.Application.UseCases.Images.CreateImage;
using Ecommerce.Contracts.Image;
using Ecommerce.Infrastructure;

public class Program
{
  public static void Main(string[] args)
  {
    try
    {
      var builder = WebApplication.CreateBuilder(args);
      {
        // Configure Serilog from within infrastructure
        builder.Host.AddHostConfigurations();

        // Swagger Api Documentation Generator
        builder.Services.AddSwaggerGen();

        builder
          .Services.ConfigureAutomapper()
          .AddApi()
          .AddApplication(builder.Configuration)
          .AddInfrastructure(builder.Configuration);
      }

      var app = builder.Build();
      {
        app.UseAuthentication().UseAuthorization();

        app.UseMiddleware<ExceptionHandlingMiddleware>()
          .UseMiddleware<FluentValidationExceptionHandlingMiddleware>();

        if (app.Environment.IsDevelopment())
        {
          app.UseSwagger();
          app.UseSwaggerUI();
        }

        app.MapControllers();

        app.Run();
      }
    }
    catch (Exception ex)
    {
      Console.WriteLine($"Server terminated unexpectedly. {ex}");
    }
  }
}
