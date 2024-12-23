/*
  Entry Point of our program
*/

namespace Api;

using Ecommerce.Application;
using Ecommerce.Infrastructure;
using Ecommerce.Presentation.Api;
using Serilog;

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

        // Api
        builder.Services.AddApi();

        // Application
        builder.Services.AddApplication();

        // Infrastructure
        builder.Services.AddInfrastructure(builder.Configuration);
      }

      var app = builder.Build();
      {
        app.UseExceptionHandler("/error");

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
      Log.Fatal(ex, "Server terminated unexpectedly.");
    }
    finally
    {
      Log.CloseAndFlush();
    }
  }
}
