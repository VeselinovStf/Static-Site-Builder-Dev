using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IMailBoxService<T>
    {
        Task<T> GetClientMailBox(string clientId);

        Task SendClientNewMessage(string clientOwnerId, string to, string subject, string text, bool isDraft);
    }
}