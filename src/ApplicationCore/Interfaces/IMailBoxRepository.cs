using ApplicationCore.Entities.MessageAggregate;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IMailBoxRepository : IAsyncRepository<MailBox>
    {
        Task<MailBox> GetClientMailBox(string id);
    }
}