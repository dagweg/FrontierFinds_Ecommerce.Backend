/*
  Entry Point of our program
*/

namespace Ecommerce.Api;

using System.Configuration;
using System.Data;
using System.Threading.Tasks;
using Ecommerce.Api;
using Ecommerce.Api.Mapping;
using Ecommerce.Api.Middlewares;
using Ecommerce.Application;
using Ecommerce.Application.Common.Utilities;
using Ecommerce.Application.UseCases.Images.Common;
using Ecommerce.Application.UseCases.Images.CreateImage;
using Ecommerce.Contracts.Image;
using Ecommerce.Domain.Common.Entities;
using Ecommerce.Domain.ProductAggregate;
using Ecommerce.Domain.UserAggregate;
using Ecommerce.Infrastructure;
using Ecommerce.Infrastructure.Common;
using Ecommerce.Infrastructure.Persistence.EfCore;
using Ecommerce.Infrastructure.Persistence.EfCore.Options;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging; // **ADD THIS using**
using Microsoft.Extensions.Options;

public class Program
{
  public static async Task Main(string[] args)
  {
    // Get a logger instance here for initial startup logging
    var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole()); // Or use your configured logger factory
    var logger = loggerFactory.CreateLogger<Program>();

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
        if (app.Environment.IsProduction())
        {
          app.UseHttpsRedirection();
        }
        app.UseRouting();
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

        await MigrateDatabase(app, logger); // **Pass logger**

        await SeedDatabase(app, logger); // **Pass logger**

        app.Run();
      }
    }
    catch (Exception ex)
    {
      logger.LogError(ex, "Server terminated unexpectedly."); // **Use logger.LogError with exception**
      // Console.WriteLine($"Server terminated unexpectedly. {ex}"); // Removed Console.WriteLine
    }
  }

  public static async Task SeedDatabase(WebApplication app, ILogger logger) // **Add ILogger parameter**
  {
    using var scope = app.Services.CreateScope();
    {
      var services = scope.ServiceProvider;
      var db = services.GetRequiredService<EfCoreContext>();
      var dbOptions = services.GetRequiredService<IOptions<DatabaseOptions>>();

      try
      {
        logger.LogInformation("Starting database seeding..."); // Log start of seeding
        await SeedCategories(db, dbOptions, logger); // **Pass logger**

        if (app.Environment.IsDevelopment())
        {
          await SeedUsers(db, logger); // **Pass logger**
          await SeedProducts(db, logger); // **Pass logger**
        }
        logger.LogInformation("Database seeding completed successfully."); // Log success
      }
      catch (Exception ex)
      {
        logger.LogError(ex, "An error occurred while seeding the database."); // **Use logger.LogError with exception**
        // Console.WriteLine($"An error occurred while seeding the database."); // Removed Console.WriteLine
        // System.Console.WriteLine(ex); // Removed System.Console.WriteLine
        throw;
      }
    }
  }

  public static async Task MigrateDatabase(WebApplication app, ILogger logger) // **Add ILogger parameter**
  {
    logger.LogInformation("Starting database migrations..."); // Log start of migrations
    try
    {
      using var scope = app.Services.CreateScope();
      var db = scope.ServiceProvider.GetRequiredService<EfCoreContext>();
      await db.Database.MigrateAsync();
      logger.LogInformation("Database migrations completed successfully."); // Log success
    }
    catch (Exception ex)
    {
      logger.LogError(ex, "Error during database migrations."); // **Use logger.LogError with exception**
      // Console.WriteLine(ex); // Removed Console.WriteLine
      throw; // Re-throw to fail startup
    }
  }

  public static async Task SeedUsers(EfCoreContext db, ILogger logger) // **Add ILogger parameter**
  {
    logger.LogInformation("Seeding Users..."); // Log start of user seeding
    // Console.WriteLine("Seeding Users"); // Removed Console.WriteLine
    var seed = Seeding.User.GetSeed();

    List<User> users = new();
    foreach (var user in seed)
    {
      // make sure no duplicate users are added
      var found = await db.Users.FindAsync(user.Id);

      if (found == null) // new
      {
        users.Add(user);
      }
      else
      {
        // update existing user
        db.Entry(found).CurrentValues.SetValues(user);
      }
    }

    await db.Users.AddRangeAsync(users);
    int i = await db.SaveChangesAsync();
    logger.LogInformation($"User seeding finished. Changes {i}"); // Log success with changes count
    // Console.WriteLine($"User seeding finished. Changes {i}"); // Removed Console.WriteLine
  }

  public static async Task SeedProducts(EfCoreContext db, ILogger logger) // **Add ILogger parameter**
  {
    logger.LogInformation("Seeding Products..."); // Log start of product seeding
    // Console.WriteLine("Seeding Products"); // Removed Console.WriteLine
    var seed = Seeding.Product.GetSeed();

    List<Product> products = new();
    foreach (var prod in seed)
    {
      // make sure no duplicate products are added
      var found = await db.Products.AsNoTracking().FirstOrDefaultAsync(p => p.Id == prod.Id);

      if (found == null) // new
      {
        products.Add(prod);
      }
      else
      {
        // update existing product
        // db.Entry(found).CurrentValues.SetValues(prod);
        db.Products.Attach(prod);
        db.Entry(prod).State = EntityState.Modified;
      }
    }

    await db.Products.AddRangeAsync(products);
    int i = await db.SaveChangesAsync();
    logger.LogInformation($"Product seeding finished. Changes: {i}"); // Log success with changes count
    // Console.WriteLine($"Product seeding finished. Changes: {i}"); // Removed Console.WriteLine
  }

  public static async Task SeedCategories(
    EfCoreContext db,
    IOptions<DatabaseOptions> dbOptions,
    ILogger logger
  ) // **Add ILogger parameter**
  {
    logger.LogInformation("Seeding Categories..."); // Log start of category seeding
    // Console.WriteLine("Seeding Categories"); // Removed Console.WriteLine
    var seed = Seeding.Categories.GetSeed();

    try
    {
      var uow = new UnitOfWork(db, new Logger<UnitOfWork>(new LoggerFactory())); // Consider getting ILoggerFactory injected if possible

      // Ensure IDENTITY_INSERT is set within the same transaction as SaveChanges
      await uow.ExecuteTransactionAsync(async () =>
      {
        // Enable IDENTITY_INSERT
        if (dbOptions.Value.Provider == DatabaseOptions.Providers.SqlServer)
          await db.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT Categories ON;");

        List<Category> categories = new();
        foreach (var category in seed)
        {
          var foundEntity = await db
            .Categories.AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == category.Id);
          if (foundEntity != null)
          {
            // Update existing entity
            // db.Entry(foundEntity).CurrentValues.SetValues(category);
            db.Categories.Attach(category);
            db.Entry(category).State = EntityState.Modified;
          }
          else
          {
            // Insert new category
            categories.Add(category);
          }
        }

        if (categories.Any())
        {
          await db.Categories.AddRangeAsync(categories);
        }

        return 1;
      });
      logger.LogInformation("Category seeding completed successfully."); // Log success
    }
    catch (Exception ex)
    {
      logger.LogError(ex, "Error seeding categories."); // **Use logger.LogError with exception**
      // Console.WriteLine($"Error seeding categories: {ex.Message}"); // Removed Console.WriteLine
      throw; // Re-throw to handle upstream
    }
    finally
    {
      // Ensure IDENTITY_INSERT is turned off (for sql server)
      if (dbOptions.Value.Provider == DatabaseOptions.Providers.SqlServer)
      {
        logger.LogInformation("Turning IDENTITY_INSERT Categories OFF (SQL Server)."); // Log IDENTITY_INSERT OFF action
        // Console.WriteLine("how is it here?"); // Removed Console.WriteLine
        await db.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT Categories OFF;");
      }
    }
  }
}
