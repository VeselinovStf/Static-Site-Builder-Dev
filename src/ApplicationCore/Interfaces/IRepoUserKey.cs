using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IRepoUserKey
    {
        Task<bool> AddKey(string accesToken, string key, string title);
    }
}