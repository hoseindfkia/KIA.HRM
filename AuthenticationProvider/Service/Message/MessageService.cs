
using System.Net.Mail;
using System.Net;
using AuthenticationProvider.Models.Main;
using Microsoft.Extensions.Options;

namespace AuthenticationProvider.Service.Message
{
    public class MessageService : IMessageService
    {
        private readonly AppSettings _mySettings;
        public MessageService(IOptions<AppSettings> mySettings)
        {
            _mySettings = mySettings.Value;
        }
        public Task SendEmailAsync(string toEmail, string subject, string message, bool isMessageHtml = false)
        {
            var EmailConfig = _mySettings.EmailConfig;
            using (var client = new SmtpClient())
            {

                var credentials = new NetworkCredential()
                {
                    UserName = EmailConfig.UserName , // without @gmail.com
                    Password = EmailConfig.Password
                };

                client.Credentials = credentials;
                client.Host = EmailConfig.Host;
                client.Port = EmailConfig.Port;
                client.EnableSsl = EmailConfig.EnableSsl;

                using var emailMessage = new MailMessage()
                {
                    To = { new MailAddress(toEmail) },
                    From = new MailAddress(EmailConfig.EmailFrom), // with @gmail.com
                    Subject = subject,
                    Body = message,
                    IsBodyHtml = isMessageHtml
                };

                client.Send(emailMessage);
            }

            return Task.CompletedTask;
        }
    }
}
