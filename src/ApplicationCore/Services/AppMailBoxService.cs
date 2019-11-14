using ApplicationCore.Entities.MessageAggregate;
using ApplicationCore.Interfaces;
using ApplicationCore.Specifications;

using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class AppMailBoxService : IAppMailBoxService
    {
        private readonly IAsyncRepository<MailBox> mailBox;
        private readonly IAppLogger<AppMailBoxService> logger;

        public AppMailBoxService(
            IAsyncRepository<MailBox> mailBox,
            IAppLogger<AppMailBoxService> logger)
        {
            this.mailBox = mailBox ?? throw new System.ArgumentNullException(nameof(mailBox));
            this.logger = logger ?? throw new System.ArgumentNullException(nameof(logger));
        }

        public async Task<MailBox> GetClientMailBox(string clientId)
        {
            var clientMailBoxSpec = new MailBoxWithMessagesSpecification(clientId);

            return this.mailBox.GetSingleBySpec(clientMailBoxSpec);
        }
    }
}