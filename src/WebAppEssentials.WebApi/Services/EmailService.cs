using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WebAppEssentials.Configurations;

namespace WebAppEssentials.Services;

/// <summary>
/// This class is responsible for sending emails using the configured SMTP settings.
/// </summary>
public class EmailService(IOptions<SmtpSettings> smtpSettings,
    ILogger<EmailService> logger) : IEmailService
{
    private readonly SmtpSettings _smtpSettings = smtpSettings.Value;

    /// <inheritdoc />
    public async Task SendMailAsync(string email, string subject, string message)
    {
        if (string.IsNullOrEmpty(_smtpSettings.SmtpServer))
        {
            logger.LogWarning("SMTP server is not configured. Email sending skipped.");
            var logMailMessage = $"Email: {email} \nSubject: {subject} \nBody: {message}";
            logger.LogInformation(logMailMessage);
            return;
        }
    
        var mailMessage = new MailMessage(_smtpSettings.SmtpFromAddress, email)
        {
            Subject = subject,
            Body = message
        };
    
        using var smtpClient = new SmtpClient(_smtpSettings.SmtpServer, _smtpSettings.SmtpPort);
        smtpClient.Credentials =
            new NetworkCredential(_smtpSettings.SmtpUsername, _smtpSettings.SmtpPassword);
        smtpClient.EnableSsl = _smtpSettings.SmtpEnableSsl;
    
        try
        {
            await smtpClient.SendMailAsync(mailMessage);
        }
        catch (Exception ex)
        {
            // Log or handle the email sending error
            logger.LogError(ex.Message);
            throw;
        }
    }
}