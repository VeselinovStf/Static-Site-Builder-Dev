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
        Task<IEnumerable<string>> GetRolesAsync(T client);
        Task<T> RetrieveUserAsync(ClaimsPrincipal user);
    }
}
