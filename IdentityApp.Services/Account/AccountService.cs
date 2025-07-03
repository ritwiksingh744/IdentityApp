using IdentityApp.Data.Entity;
using IdentityApp.Models.Account;
using Microsoft.AspNetCore.Identity;

namespace IdentityApp.Services.Account
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountService(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IdentityResult> RegisterAsync(RegisterViewModel model)
        {
            var user = new User {FirstName=model.FirstName, LastName=model.LastName, UserName = model.Email, Email = model.Email, EmailConfirmed = true };
            var data = await _userManager.CreateAsync(user, model.Password);
            if (data.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "User");
            }
            return data;
        }

        public async Task<SignInResult> LoginAsync(LoginViewModel model)
        {
            return await _signInManager.PasswordSignInAsync(
                model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<string?> GenerateResetTokenAsync(ForgotPasswordViewModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null) return null;

            return await _userManager.GeneratePasswordResetTokenAsync(user);
        }

        public async Task<IdentityResult> ResetPasswordAsync(ResetPasswordViewModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null) return IdentityResult.Failed();

            return await _userManager.ResetPasswordAsync(user, model.Token, model.Password);
        }

        public async Task<IdentityResult> ChangePasswordAsync(ChangePasswordViewModel model, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return IdentityResult.Failed();

            return await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
        }
    }
}
