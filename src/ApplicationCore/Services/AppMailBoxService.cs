﻿using ApplicationCore.Entities.MessageAggregate;
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

        public async Task SendClientMessage(string clientOwnerId, string from, bool IsNew, bool IsDraft, bool IsTrash, DateTime sendDate, string subject, string text, string to)
        {
            var mailBox = await this.mailBoxRepository.GetByIdAsync(clientOwnerId);

            // mailBox.AddItem(from, to, subject, text, sendDate, IsDraft, IsTrash, IsNew);

            // await this.mailBoxRepository.UpdateAsync(mailBox);
        }
    }
}