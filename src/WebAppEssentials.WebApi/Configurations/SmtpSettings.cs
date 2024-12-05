namespace WebAppEssentials.Configurations;

/// <summary>
/// Represents the SMTP settings for sending emails.
/// </summary>
public class SmtpSettings
{
    /// <summary>
    /// The SMTP server address.
    /// </summary>
    public required string SmtpServer { get; set; }

    /// <summary>
    /// The SMTP server port number.
    /// </summary>
    public int SmtpPort { get; set; }

    /// <summary>
    /// The username for authentication with the SMTP server.
    /// </summary>
    public required string SmtpUsername { get; set; }

    /// <summary>
    /// The password for authentication with the SMTP server.
    /// </summary>
    public required string SmtpPassword { get; set; }

    /// <summary>
    /// Indicates whether SSL should be enabled for the SMTP connection.
    /// </summary>
    public bool SmtpEnableSsl { get; set; }

    /// <summary>
    /// The email address to use as the sender.
    /// </summary>
    public required string SmtpFromAddress { get; set; }
}