using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IMessageService<T>
    {
        Task<T> MarkMessageAsReadedAsync(string clientOwnerId, string messageId);

        Task SendClientNewMessage(string clientOwnerId, string to, string subject, string text, bool isDraft);

        Task MarkMessageAsTrashedAsync(string clientId, string messageId);

        Task MarkMessageAsDeletedAsync(string clientId, string messageId);
    }
}