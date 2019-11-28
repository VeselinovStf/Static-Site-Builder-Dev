using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IHubKeyMaker<T>
    {
        Task<T> CreateKey(string accesToken);
    }
}