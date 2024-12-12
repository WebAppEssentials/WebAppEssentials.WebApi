using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebAppEssentials.Middlewares;
using WebAppEssentials.WebApi.Data;

namespace WebAppEssentials.Extensions;

public static class ApplicationBuilderExtensions
{
    /// <summary>
    /// Adds a global exception handling middleware to the application's request processing pipeline.
    /// </summary>
    /// <param name="app">The <see cref="IApplicationBuilder"/> instance to add the middleware to.</param>
    /// <remarks>
    /// This middleware catches and handles exceptions that occur during the request processing pipeline.
    /// It logs the exception details and returns a meaningful response to the client.
    /// </remarks>
    public static void UseGlobalExceptionHandling(this IApplicationBuilder app)
    {
        app.UseMiddleware<ExceptionHandlingMiddleware>();
    }
    
    public static void ApplyMigrations<TContext>(this WebApplication app) where TContext : BaseDbContext
    {
        // Automatically apply migrations
        using (var scope = app.Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<TContext>();
            dbContext.Database.Migrate(); // Applies pending migrations
        }
    }
}