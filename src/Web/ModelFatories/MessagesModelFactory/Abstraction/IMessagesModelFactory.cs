using Infrastructure.Messages.DTOs;
using Web.ViewModels.Messages;

namespace Web.ModelFatories.MessagesModelFactory.Abstraction
{
    public interface IMessagesModelFactory
    {
        MailBoxViewModel Create(MailBoxDTO inputModel);
    }
}