using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IHostingHubKeyMaker<T>
    {
        Task<T> CreateKey(string accesToken);
    }
}