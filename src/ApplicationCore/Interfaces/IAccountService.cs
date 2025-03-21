﻿using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IAccountService<T>
    {
        Task<T> RegisterAccountAsync(string userName, string email, string password);

        bool IsSignedIn(ClaimsPrincipal user);

        Task<IEnumerable<string>> GetRolesAsync(T user);

        Task<T> RetrieveUserAsync(ClaimsPrincipal user);

        Task<string> GenerateEmailConfirmationTokenAsync(T user);

        Task<string> GeneratePasswordResetTokenAsync(T user);

        Task SignInAsync(T user, bool isPersistent);

        Task AddToRoleAsync(T user, string role);

        Task<T> FindByIdAsync(string userId);

        Task<bool> ConfirmEmailAsync(string userId, string code);

        Task SignOutAsync();

        Task PasswordSignInAsync(string email, string password, bool rememberMe, bool lockoutOnFailure);
        Task<bool> GetClientIsInTutorial(string clientId);
        Task<T> FindByEmailAsync(string email);

        Task<bool> ConfirmCangePasswordAsync(string userId, string code);

        Task<bool> ResetPasswordAsync(string userName, string password, string confirmPassword, string token);
        Task<bool> ChangeTutorialAsync(T client);
        Task<bool> UpdateUserName(T user, string newUserName);

        Task<bool> DeleteUser(T user);

        Task<T> GetPaymentsAsync(T user);

        Task<bool> ChangeEmailAsync(T user, string userName, string emailUpdateConfirmationCode);

        Task<T> FindByUserNameAsync(string to);
    }
}