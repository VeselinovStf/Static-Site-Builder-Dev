using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IHostDeployToken<T>
    {
        Task<T> CreateDeployKey(string accesToken);
    }
}