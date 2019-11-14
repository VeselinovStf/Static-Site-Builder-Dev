using ApplicationCore.Entities.MessageAggregate;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IAppMailBoxService
    {
        Task<MailBox> GetClientMailBox(string clientId);
    }
}