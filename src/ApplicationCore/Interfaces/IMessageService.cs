using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IMessageService<T>
    {
        Task<T> MarkMessageAsReadedAsync(string clientOwnerId, string messageId);
    }
}