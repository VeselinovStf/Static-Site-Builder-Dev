using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IMailMessageService<T>
    {
        Task<T> GetMessage(string clientId, string messageId);
    }
}