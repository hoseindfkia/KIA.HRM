namespace AuthenticationProvider.Service.Message
{
    public interface IMessageService
    {
        public Task SendEmailAsync(string toEmail, string subject, string message, bool isMessageHtml = false);
    }
}
