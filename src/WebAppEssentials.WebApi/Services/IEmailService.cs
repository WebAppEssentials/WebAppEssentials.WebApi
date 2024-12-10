namespace WebAppEssentials.Services;

/// <summary>
/// Defines a contract for sending emails.
/// </summary>
public interface IEmailService
{
    /// <summary>
    /// Sends an email asynchronously.
    /// </summary>
    /// <param name="email">The recipient's email address.</param>
    /// <param name="subject">The subject of the email.</param>
    /// <param name="message">The content of the email.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task SendMailAsync(string email, string subject, string message);
}