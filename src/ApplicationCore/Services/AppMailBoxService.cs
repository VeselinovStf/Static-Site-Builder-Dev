using ApplicationCore.Entities.MessageAggregate;
using ApplicationCore.Interfaces;
using ApplicationCore.Specifications;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class AppMailBoxService : IAppMailBoxService
    {
        private readonly IAsyncRepository<MailBox> mailBoxRepository;

        public AppMailBoxService(
            IAsyncRepository<MailBox> mailBoxRepository
           )
        {
            this.mailBoxRepository = mailBoxRepository ?? throw new System.ArgumentNullException(nameof(mailBoxRepository));
        }

        public async Task DeleteMessage(string clientId, string messageId)
        {
            var clientMailBoxSpec = new MailBoxWithMessagesSpecification(clientId);

            var mailBox = this.mailBoxRepository.GetSingleBySpec(clientMailBoxSpec);

            var message = mailBox.Messages.ToList().FirstOrDefault(m => m.Id == messageId);

            message.IsDeleted = true;

            await this.mailBoxRepository.UpdateAsync(mailBox);
        }

        public async Task<MailBox> GetClientMailBox(string clientId)
        {
            var clientMailBoxSpec = new MailBoxWithMessagesSpecification(clientId);

            return this.mailBoxRepository.GetSingleBySpec(clientMailBoxSpec);
        }

        public async Task<Message> ReadMessage(string clientOwnerId, string messageId)
        {
            var clientMailBoxSpec = new MailBoxWithMessagesSpecification(clientOwnerId);

            var mailBox = this.mailBoxRepository.GetSingleBySpec(clientMailBoxSpec);

            var message = mailBox.Messages.ToList().FirstOrDefault(m => m.Id == messageId);

            message.IsNew = false;

            await this.mailBoxRepository.UpdateAsync(mailBox);

            return message;
        }

        public async Task SendClientMessage(string clientOwnerId,
            string from, bool IsNew, bool IsDraft, bool IsTrash, bool isSent,
            DateTime sendDate, string subject, string text, string to)
        {
            var clientMailBoxSpec = new MailBoxWithMessagesSpecification(clientOwnerId);

            var mailBox = this.mailBoxRepository.GetSingleBySpec(clientMailBoxSpec);

            mailBox.AddItem(from, to, subject, text, sendDate, IsDraft, IsTrash, IsNew, isSent);

            await this.mailBoxRepository.UpdateAsync(mailBox);
        }

        public async Task TrashMessage(string clientId, string messageId)
        {
            var clientMailBoxSpec = new MailBoxWithMessagesSpecification(clientId);

            var mailBox = this.mailBoxRepository.GetSingleBySpec(clientMailBoxSpec);

            var message = mailBox.Messages.ToList().FirstOrDefault(m => m.Id == messageId);

            message.IsTrash = true;
            message.IsDraft = false;
            message.IsNew = false;

            await this.mailBoxRepository.UpdateAsync(mailBox);
        }
    }
}