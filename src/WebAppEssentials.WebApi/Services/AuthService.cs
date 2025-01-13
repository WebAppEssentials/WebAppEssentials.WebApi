using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using WebAppEssentials.Configurations;
using WebAppEssentials.Entities;
using WebAppEssentials.Models.Authentication;

namespace WebAppEssentials.Services;

public class AuthService(ILogger<AuthService> logger,
    UserManager<AppUser> userManager,
    IConfiguration configuration,
    IEmailService emailService) : IAuthService
{
    public async Task<IdentityResult> RegisterUserAsync(UserRegisterDto userDto, CancellationToken cancellationToken = default)
    {
        var user = new AppUser
        {
            UserName = userDto.Email,
            Email = userDto.Email,
            FirstName = userDto.FirstName,
            LastName = userDto.LastName,
            EmailConfirmed = false
        };

        var result = await userManager.CreateAsync(user, userDto.Password);

        if (result.Succeeded) await SendEmailConfirmationAsync(user);

        return result;    
    }

    public async Task<(IdentityResult, AuthResponseDto?)> LoginUserAsync(UserLoginDto userDto, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Login attempt for {userDto.Email}", userDto.Email);

        var user = await userManager.FindByEmailAsync(userDto.Email);
        if (user == null)
            return (IdentityResult.Failed(new IdentityError
            {
                Code = "InvalidLoginAttempt",
                Description = "Invalid login attempt."
            }), null);

        var passwordValid = await userManager.CheckPasswordAsync(user, userDto.Password);
        if (!passwordValid)
            return (IdentityResult.Failed(new IdentityError
            {
                Code = "InvalidLoginAttempt",
                Description = "Invalid login attempt."
            }), null);

        if (user is not { EmailConfirmed: true })
            return (IdentityResult.Failed(new IdentityError
            {
                Code = "EmailNotConfirmed",
                Description = "Email confirmation is required."
            }), null);

        var tokenString = await GenerateToken(user);

        var response = new AuthResponseDto
        {
            Email = user.Email,
            Token = tokenString,
            UserId = user.Id
        };

        return (IdentityResult.Success, response);    }

    public Task<bool> InitiatePasswordResetAsync(string email)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ResetPasswordAsync(string email, string token, string newPassword)
    {
        throw new NotImplementedException();
    }

    public Task<IdentityResult> ConfirmEmail(string token, string email)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ResendConfirmationEmailAsync(string email)
    {
        throw new NotImplementedException();
    }
    
     private async Task SendEmailConfirmationAsync(AppUser user)
    {
        // Generate a confirmation token
        var token = await userManager.GenerateEmailConfirmationTokenAsync(user);

        // Create the confirmation link
        var applicationBaseUrl = configuration["ApplicationBaseUrl"];
        var confirmationLink =
            $"{applicationBaseUrl}confirm-email?token={WebUtility.UrlEncode(token)}&email={WebUtility.UrlEncode(user.Email)}";

        const string mailSubject = "Confirm Your Email Address";
        var mailBody =
            $"Please confirm your email address by clicking the link: <a href=\"{confirmationLink}\">Confirm Email</a>";

        try
        {
            await emailService.SendMailAsync(user.Email, mailSubject, mailBody);
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message);
            throw;
        }
    }

    private async Task<string> GenerateToken(AppUser user)
    {
        ArgumentNullException.ThrowIfNull(user);
        var securityKey =
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:SigningKey"] ??
                                                            throw new InvalidOperationException()));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var roles = await userManager.GetRolesAsync(user);
        var roleClaims = roles.Select(x => new Claim(ClaimTypes.Role, x)).ToList();

        var userClaims = await userManager.GetClaimsAsync(user);

        var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Sub, user.UserName!),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new(JwtRegisteredClaimNames.Email, user.Email!),
                new(AppConstants.Auth.ClaimTypes.ClaimTypeUid, user.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);

        var token = new JwtSecurityToken(configuration["JwtSettings:Issuer"],
            configuration["JwtSettings:Audience"],
            claims,
            expires: DateTime.UtcNow.AddHours(Convert.ToInt32(configuration["JwtSettings:Duration"])),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}