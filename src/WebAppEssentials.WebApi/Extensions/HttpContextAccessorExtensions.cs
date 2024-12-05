using Microsoft.AspNetCore.Http;
using WebAppEssentials.Configurations;

namespace WebAppEssentials.Extensions;

public static class HttpContextAccessorExtensions
{
    /// <summary>
    /// Retrieves the current user's identifier from the HTTP context.
    /// </summary>
    /// <param name="httpContextAccessor">The HTTP context accessor to retrieve the user's claims.</param>
    /// <returns>The user's identifier if found in the claims; otherwise, null.</returns>
    public static string? GetCurrentUserId(this IHttpContextAccessor httpContextAccessor) {
        return httpContextAccessor.HttpContext?.User.Claims
            .FirstOrDefault(x => x.Type == AppConstants.Auth.ClaimTypes.ClaimTypeUid)
            ?.Value;
    }
}