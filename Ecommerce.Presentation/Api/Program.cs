namespace Api;

using Ecommerce.Application;
using Ecommerce.Infrastructure;
using Microsoft.AspNetCore.Mvc.Infrastructure;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        {
            builder.Services.AddControllers();
            builder.Services.AddSwaggerGen();

            builder.Services.AddSingleton<ProblemDetailsFactory, CustomProblemDetailsFactory>();

            builder.Services.AddApplication();
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
}
