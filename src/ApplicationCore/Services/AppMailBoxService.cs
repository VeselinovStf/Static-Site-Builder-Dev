using ApplicationCore.Entities.MessageAggregate;
using ApplicationCore.Interfaces;
using ApplicationCore.Specifications;
using System;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class AppMailBoxService : IAppMailBoxService
    {
        private readonly IAsyncRepository<MailBox> mailBoxRepository;
        private readonly IAppLogger<AppMailBoxService> logger;

        public AppMailBoxService(
            IAsyncRepository<MailBox> mailBoxRepository,
            IAppLogger<AppMailBoxService> logger)
        {
            this.mailBoxRepository = mailBoxRepository ?? throw new System.ArgumentNullException(nameof(mailBoxRepository));
            this.logger = logger ?? throw new System.ArgumentNullException(nameof(logger));
        }

        public async Task<MailBox> GetClientMailBox(string clientId)
        {
            var clientMailBoxSpec = new MailBoxWithMessagesSpecification(clientId);

            return this.mailBoxRepository.GetSingleBySpec(clientMailBoxSpec);
        }

        public async Task<Message> GetMessage(string clientId, string messageId)
        {
            throw new NotImplementedException();
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
    }
}