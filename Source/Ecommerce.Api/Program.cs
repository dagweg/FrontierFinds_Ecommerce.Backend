/*
  Entry Point of our program
*/

namespace Ecommerce.Api;

using System.Configuration;
using Ecommerce.Api;
using Ecommerce.Api.Mapping;
using Ecommerce.Api.Middlewares;
using Ecommerce.Application;
using Ecommerce.Application.UseCases.Images.Common;
using Ecommerce.Application.UseCases.Images.CreateImage;
using Ecommerce.Contracts.Image;
using Ecommerce.Infrastructure;
using Ecommerce.Infrastructure.Common;
using Microsoft.AspNetCore.Cors.Infrastructure;

public class Program
{
  public static void Main(string[] args)
  {
    try
    {
      var builder = WebApplication.CreateBuilder(args);
      {
        builder.Services.AddCors(o =>
        {
          o.AddDefaultPolicy(b =>
            b.WithOrigins(
                builder.Configuration.GetConnectionString(
                  $"{ClientSettings.SectionName}:ClientBaseUrl"
                ) ?? "http://localhost:3000"
              )
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials()
          );
        });

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
        app.UseCors();
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
