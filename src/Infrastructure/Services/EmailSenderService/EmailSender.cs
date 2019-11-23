using ApplicationCore.Interfaces;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;

namespace Infrastructure.Services.EmailSenderService
{
    public class EmailSender : IEmailSender
    {
        public EmailSender(IOptions<AuthMessageSenderOptions> optionsAccessor)
        {
            Options = optionsAccessor.Value;
        }

        public AuthMessageSenderOptions Options { get; } //set only via Secret Manager

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            await Execute(Options.SendGridKey, Options.SendGridUser, subject, message, email);
        }

        public async Task Execute(string apiKey, string systemUserEmail, string subjectTxt, string messageBody, string email)
        {
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("SSB_Admin@vCorp.com", "Administration User");
            var subject = subjectTxt;
            var to = new EmailAddress(email, "Example User");
            var plainTextContent = "and easy to do anywhere, even with C#";
            var htmlContent = messageBody;
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
        }
    }
}