using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IAppSignInManager<T>
    {
        Task SignInAsync(T user, bool isPersistent);
        bool IsSignedIn(ClaimsPrincipal user, bool isPersistent);
        Task SignOutAsync();
        Task<SignInResult> PasswordSignInAsync(string email, string password, bool rememberMe, bool lockoutOnFailure);
    }
}
