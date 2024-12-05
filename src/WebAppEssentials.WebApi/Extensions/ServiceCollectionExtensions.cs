using System.Threading.RateLimiting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebAppEssentials.Configurations;
using WebAppEssentials.Services;

namespace WebAppEssentials.Extensions;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds email sender functionality to the application's service collection.
    /// </summary>
    /// <param name="services">The service collection to add the email sender to.</param>
    /// <param name="configuration">The application's configuration.</param>
    /// <returns>The service collection with the email sender added.</returns>
    public static IServiceCollection AddEmailSender(this IServiceCollection services, IConfiguration configuration)
    {
        // Configure SMTP settings from the application's configuration.
        services.Configure<SmtpSettings>(configuration.GetSection("SmtpSettings"));
        
        // Add a scoped implementation of IEmailSender using EmailSender class.
        services.AddScoped<IEmailSender, EmailSender>();
        
        // Return the updated service collection.
        return services;
    }

    /// <summary>
    /// Adds rate limiting functionality to the application's service collection.
    /// </summary>
    /// <param name="services">The service collection to add the rate limiter to.</param>
    /// <param name="configuration">The application's configuration, which contains rate limit settings.</param>
    /// <returns>The service collection with the rate limiter added.</returns>
    public static IServiceCollection AddRateLimiting(this IServiceCollection services, IConfiguration configuration)
    {
        // TODO: error handling if no configuration is provided
        
        services.AddRateLimiter(options =>
        {
            options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(httpContext =>
                RateLimitPartition.GetFixedWindowLimiter(
                    partitionKey: httpContext.User.Identity?.Name ?? httpContext.Request.Headers.Host.ToString(),
                    factory: partition => new FixedWindowRateLimiterOptions
                    {
                        AutoReplenishment = true,
                        PermitLimit = Convert.ToInt32(configuration["RateLimit:Limit"]),
                        QueueLimit = 0,
                        Window = TimeSpan.FromMinutes(Convert.ToDouble(configuration["RateLimit:WindowInMinutes"])),
                    }));
            options.RejectionStatusCode = Convert.ToInt32(configuration["RateLimit:HttpStatusCode"]);
        });
        
        return services;
    }
}