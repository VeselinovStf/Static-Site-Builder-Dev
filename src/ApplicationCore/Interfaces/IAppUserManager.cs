using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
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
    }
}
