using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IMailBoxService<T>
    {
        Task<T> GetClientMailBox(string clientId);
    }
}