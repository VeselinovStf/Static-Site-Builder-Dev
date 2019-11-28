using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IRepoHubKeyMaker
    {
        Task<bool> CreateKey(string accesToken, string key, string title);
    }
}