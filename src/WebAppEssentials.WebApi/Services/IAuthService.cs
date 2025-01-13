using Microsoft.AspNetCore.Identity;
using WebAppEssentials.Models.Authentication;

namespace WebAppEssentials.Services;

public interface IAuthService
{
    Task<IdentityResult> RegisterUserAsync(UserRegisterDto userDto, CancellationToken cancellationToken = default);
    Task<(IdentityResult, AuthResponseDto?)> LoginUserAsync(UserLoginDto userDto, CancellationToken cancellationToken = default);

    Task<bool> InitiatePasswordResetAsync(string email);
    Task<bool> ResetPasswordAsync(string email, string token, string newPassword);
    Task<IdentityResult> ConfirmEmail(string token, string email);
    Task<bool> ResendConfirmationEmailAsync(string email);
}