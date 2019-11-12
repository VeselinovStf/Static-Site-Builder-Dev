using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Infrastructure.Identity
{
    public class UserManagerAdapter : IAppUserManager<ApplicationUser>
    {
        private readonly UserManager<ApplicationUser> userManager;

        public UserManagerAdapter(
            UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        public async Task<IdentityResult> CreateAsync(ApplicationUser user, string password)
        {
            return await this.userManager.CreateAsync(user, password);
        }

        public async Task<string> GenerateEmailConfirmationTokenAsync(ApplicationUser user)
        {
            return await this.userManager.GenerateEmailConfirmationTokenAsync(user);
        }

        public async Task AddToRoleAsync(ApplicationUser user, string role)
        {
            await this.userManager.AddToRoleAsync(user, role);
        }

        public async Task<IEnumerable<string>> GetRolesAsync(ApplicationUser user)
        {
            return await this.userManager.GetRolesAsync(user);
        }

        public async Task<ApplicationUser> GetUserAsync(ClaimsPrincipal user)
        {
            return await this.userManager.GetUserAsync(user);
        }

        public async Task<ApplicationUser> FindByIdAsync(string userId)
        {
            return await this.userManager.FindByIdAsync(userId);
        }

        public Task<IdentityResult> ConfirmEmailAsync(ApplicationUser user, string code)
        {
            return this.userManager.ConfirmEmailAsync(user, code);
        }

        public async Task<ApplicationUser> FindByEmailAsync(string email)
        {
            return await this.userManager.Users.FirstOrDefaultAsync(u => u.Email == email && !u.IsDeleted);
        }

        public async Task<string> GeneratePasswordResetTokenAsync(ApplicationUser user)
        {
            return await this.userManager.GeneratePasswordResetTokenAsync(user);
        }

        public async Task<IdentityResult> ResetPasswordAsync(ApplicationUser user, string token, string password)
        {
            return await this.userManager.ResetPasswordAsync(user, token, password);
        }

        public async Task<IdentityResult> ChangePasswordAsync(ApplicationUser user, string currentPassword, string newPassword)
        {
            return await this.userManager.ChangePasswordAsync(user, currentPassword, newPassword);
        }

        public async Task<IdentityResult> ChangeEmailNameAsync(ApplicationUser user, string newEmail, string token)
        {
            return await this.userManager.ChangeEmailAsync(user, newEmail, token);
        }

        public async Task<IdentityResult> ChangeUserNameAsync(ApplicationUser user, string userName)
        {
            return await this.userManager.SetUserNameAsync(user, userName);
        }

        //TODO: Test This
        public async Task DeleteClient(string userId)
        {
            var user = await this.userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);

            user.IsDeleted = true;
        }

        public async Task<ApplicationUser> FindByNameAsync(string userName)
        {
            return await this.userManager.FindByNameAsync(userName);
        }
    }
}