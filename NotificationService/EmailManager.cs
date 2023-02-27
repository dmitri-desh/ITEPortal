using MailKit.Net.Smtp;
using Microsoft.Extensions.Logging;
using MimeKit;

namespace MessengerService
{
    public class EmailManager : IEmailManager
    {
        private readonly ILogger _logger;
        public EmailManager(ILogger<EmailManager> logger) => _logger = logger;
        public async Task SendMessage(NetworkSettings networkSettings, EmailMessage emailMessage, string username = "", string password = "")
        {
            var builder = new BodyBuilder
            {
                HtmlBody = emailMessage.Body
            };

            var message = new MimeMessage
            {
                Subject = emailMessage.Subject,
                Body = builder.ToMessageBody()
            };
            message.From.Add(new MailboxAddress(emailMessage.FromEmail, emailMessage.FromEmail));
            message.To.Add(new MailboxAddress(emailMessage.ToEmail, emailMessage.ToEmail));

            using (var client = new SmtpClient())
            {
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                var authNeeded = !string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password);

                try
                {
                    await client.ConnectAsync(networkSettings.Host, networkSettings.Port, authNeeded);

                    if (authNeeded)
                    {
                        await client.AuthenticateAsync(username, password);
                    }

                    await client.SendAsync(message);
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error in sending a message. {ex.Message}. From {emailMessage.FromEmail}. ");
                }

                await client.DisconnectAsync(true);
            }
        }

    }
}
