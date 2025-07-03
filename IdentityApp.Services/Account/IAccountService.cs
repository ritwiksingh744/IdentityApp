using IdentityApp.Models.Account;
using Microsoft.AspNetCore.Identity;

namespace IdentityApp.Services.Account
{
    public interface IAccountService
    {
        Task<IdentityResult> RegisterAsync(RegisterViewModel model);
        Task<SignInResult> LoginAsync(LoginViewModel model);
        Task LogoutAsync();
        Task<string?> GenerateResetTokenAsync(ForgotPasswordViewModel model);
        Task<IdentityResult> ResetPasswordAsync(ResetPasswordViewModel model);
        Task<IdentityResult> ChangePasswordAsync(ChangePasswordViewModel model, string userId);
    }
}
