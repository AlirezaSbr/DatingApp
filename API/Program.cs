using System.Text;
using API.Data;
using API.Extentions;
using API.Interfaces;
using API.Middleware;
using API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.


        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddAplicationServices(builder.Configuration);

        builder.Services.AddControllers();
        builder.Services.AddCors();
        builder.Services.AddIdentityServices(builder.Configuration);
        var app = builder.Build();


        using var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;
        try
        {
            var context = services.GetRequiredService<DataContext>();
            await context.Database.MigrateAsync();
            await Seed.SeedUsers(context);
        }
        catch (System.Exception ex)
        {
            var logger = services.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "Failed to migrate");
        }

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
             app.UseSwagger();
             app.UseSwaggerUI();
        }

        app.UseMiddleware<ExceptionMiddleware>();
        app.UseHttpsRedirection();
        app.UseCors(x =>
        {
            x.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200");
        });
        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        await app.RunAsync();
    }
}