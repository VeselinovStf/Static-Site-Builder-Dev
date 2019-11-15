using ApplicationCore.Entities.MessageAggregate;
using System;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IAppMailBoxService
    {
        Task<MailBox> GetClientMailBox(string clientId);

        Task SendClientMessage(
            string clientOwnerId, string from, bool IsNew,
            bool IsDraft, bool IsTrash, bool isSent, DateTime sendDate,
            string subject, string text, string to);

        Task<Message> GetMessage(string clientId, string messageId);
    }
}