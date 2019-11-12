using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Infrastructure.Identity
{
    public class SignInManagerAdapter : IAppSignInManager<ApplicationUser>
    {
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly UserManager<ApplicationUser> userManagerTest;

        public SignInManagerAdapter(
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManagerTest
            )
        {
            this.signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
            this.userManagerTest = userManagerTest ?? throw new ArgumentNullException(nameof(userManagerTest));
        }

        public bool IsSignedIn(ClaimsPrincipal user, bool isPersistent)
        {
            return this.signInManager.IsSignedIn(user);
        }

        public async Task<SignInResult> PasswordSignInAsync(string userName, string password, bool rememberMe, bool lockoutOnFailure)
        {
            return await this.signInManager.PasswordSignInAsync(userName, password, rememberMe, lockoutOnFailure);
        }

        public async Task SignInAsync(ApplicationUser user, bool isPersistent)
        {
            await this.signInManager.SignInAsync(user, isPersistent);
        }

        public async Task SignOutAsync()
        {
            await this.signInManager.SignOutAsync();
        }
    }
}