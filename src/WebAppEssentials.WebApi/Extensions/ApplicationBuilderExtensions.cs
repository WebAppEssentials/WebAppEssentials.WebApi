using Microsoft.AspNetCore.Builder;
using WebAppEssentials.Middlewares;

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
}