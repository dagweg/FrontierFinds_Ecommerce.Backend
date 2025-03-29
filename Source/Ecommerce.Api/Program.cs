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
using Microsoft.Extensions.Options;

public class Program
{
  public static async Task Main(string[] args)
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

        await MigrateDatabase(app);

        await SeedDatabase(app);

        app.Run();
      }
    }
    catch (Exception ex)
    {
      Console.WriteLine($"Server terminated unexpectedly. {ex}");
    }
  }

  public static async Task SeedDatabase(WebApplication app)
  {
    using var scope = app.Services.CreateScope();
    {
      var services = scope.ServiceProvider;
      var db = services.GetRequiredService<EfCoreContext>();
      var dbOptions = services.GetRequiredService<IOptions<DatabaseOptions>>();

      try
      {
        await SeedCategories(db, dbOptions);

        if (app.Environment.IsDevelopment())
        {
          await SeedUsers(db);
          await SeedProducts(db);
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine($"An error occurred while seeding the database.");
        System.Console.WriteLine(ex);
        throw;
      }
    }
  }

  public static async Task MigrateDatabase(WebApplication app)
  {
    try
    {
      Console.WriteLine("Performing Migrations");
      using var scope = app.Services.CreateScope();
      var db = scope.ServiceProvider.GetRequiredService<EfCoreContext>();
      await db.Database.MigrateAsync();
    }
    catch (Exception ex)
    {
      Console.WriteLine(ex);
    }
  }

  public static async Task SeedUsers(EfCoreContext db)
  {
    Console.WriteLine("Seeding Users");
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
    Console.WriteLine($"User seeding finished. Changes {i}");
  }

  public static async Task SeedProducts(EfCoreContext db)
  {
    Console.WriteLine("Seeding Products");
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
    Console.WriteLine($"Product seeding finished. Changes: {i}");
  }

  public static async Task SeedCategories(EfCoreContext db, IOptions<DatabaseOptions> dbOptions)
  {
    Console.WriteLine("Seeding Categories");
    var seed = Seeding.Categories.GetSeed();

    try
    {
      var uow = new UnitOfWork(db, new Logger<UnitOfWork>(new LoggerFactory()));

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
    }
    catch (Exception ex)
    {
      Console.WriteLine($"Error seeding categories: {ex.Message}");
      throw; // Re-throw to handle upstream
    }
    finally
    {
      // Ensure IDENTITY_INSERT is turned off (for sql server)
      if (dbOptions.Value.Provider == DatabaseOptions.Providers.SqlServer)
      {
        Console.WriteLine("how is it here?");
        await db.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT Categories OFF;");
      }
    }
  }
}
