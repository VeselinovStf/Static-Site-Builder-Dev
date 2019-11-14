using ApplicationCore.Entities.MessageAggregate;

namespace ApplicationCore.Specifications
{
    public sealed class MailBoxWithMessagesSpecification : BaseSpecification<MailBox>
    {
        public MailBoxWithMessagesSpecification(string clientId)
            : base(mb => mb.ClientId == clientId)
        {
            AddInclude(m => m.Messages);
        }
    }
}