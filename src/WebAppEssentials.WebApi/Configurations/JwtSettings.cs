namespace WebAppEssentials.Configurations;

/// <summary>
/// Represents the settings for JSON Web Tokens (JWT) used in the application.
/// </summary>
public class JwtSettings
{
    /// <summary>
    /// Gets or sets the signing key for JWT generation and verification.
    /// </summary>
    /// <value>The signing key.</value>
    public required string SigningKey { get; set; }

    /// <summary>
    /// Gets or sets the issuer of the JWT.
    /// </summary>
    /// <value>The issuer.</value>
    public required string Issuer { get; set; }

    /// <summary>
    /// Gets or sets the audience for the JWT.
    /// </summary>
    /// <value>The audience.</value>
    public required string Audience { get; set; }

    /// <summary>
    /// Gets or sets the duration (in minutes) for which the JWT is valid.
    /// </summary>
    /// <value>The duration in minutes.</value>
    public int Duration { get; set; }
}