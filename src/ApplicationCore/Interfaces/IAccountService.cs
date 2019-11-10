using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
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
        Task SignInAsync(T user, bool isPersistent);

        Task AddToRoleAsync(T user, string role);
        Task<T> FindByIdAsync(string userId);
        Task<bool> ConfirmEmailAsync(T user, string code);
        Task SignOutAsync();
        Task PasswordSignInAsync(string email, string password, bool rememberMe, bool lockoutOnFailure);
    }
}
