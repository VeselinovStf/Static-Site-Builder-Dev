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
    }
}
