/*
  Entry Point of our program
*/

namespace Api;

using Ecommerce.Api;
using Ecommerce.Api.Mapping;
using Ecommerce.Api.Middlewares;
using Ecommerce.Application;
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
        builder.Host.AddInfrastructure();

        // Swagger Api Documentation Generator
        builder.Services.AddSwaggerGen();

        builder
          .Services.ConfigureAutomapper()
          .AddApi()
          .AddApplication()
          .AddInfrastructure(builder.Configuration);
      }

      var app = builder.Build();
      {
        app.UseAuthentication().UseAuthorization();

        app.UseMiddleware<FluentValidationExceptionHandlingMiddleware>()
          .UseMiddleware<ExceptionHandlingMiddleware>()
          .UseMiddleware<DomainExceptionHandlingMiddleware>();

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
