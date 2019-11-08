using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Identity
{
    public class SignInManagerAdapter : IAppSignInManager<ApplicationUser>
    {
        private readonly SignInManager<ApplicationUser> signInManager;

        public SignInManagerAdapter(
            SignInManager<ApplicationUser> signInManager
            )
        {
            this.signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
        }

        public bool IsSignedIn(ClaimsPrincipal user, bool isPersistent)
        {
            return this.signInManager.IsSignedIn(user);
        }

        public async Task SignInAsync(ApplicationUser user, bool isPersistent)
        {
            await this.signInManager.SignInAsync(user, isPersistent);
        }
    }
}
