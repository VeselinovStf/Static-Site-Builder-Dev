﻿using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IAppUserManager<T>
    {
        Task<IdentityResult> CreateAsync(T user, string password);

        Task<string> GenerateEmailConfirmationTokenAsync(T user);

        Task AddToRoleAsync(T user, string role);

        Task<IEnumerable<string>> GetRolesAsync(T user);

        Task<T> GetUserAsync(ClaimsPrincipal user);

        Task<T> FindByIdAsync(string userId);

        Task<IdentityResult> ConfirmEmailAsync(T user, string code);

        Task<T> FindByEmailAsync(string email);

        Task<string> GeneratePasswordResetTokenAsync(T user);

        Task<IdentityResult> ResetPasswordAsync(T user, string token, string password);

        Task<IdentityResult> ChangePasswordAsync(T user, string currentPassword, string newPassword);

        Task<IdentityResult> ChangeEmailNameAsync(T user, string newEmail, string token);

        Task<IdentityResult> ChangeTutorialStatus(T user);

        Task<IdentityResult> ChangeUserNameAsync(T user, string userName);

        Task<IdentityResult> DeleteClient(string userId);

        Task<T> FindByNameAsync(string userName);
    }
}