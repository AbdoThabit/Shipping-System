using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Sockets;
using MimeKit;
using MailKit.Net.Smtp;
using MailKit.Security;

namespace Shipping_System.BL.Helper
{
    public class MailHelper : IMailHelper
    {
        private readonly MailSettings _mailSettings;
        public MailHelper(IOptions<MailSettings> mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }
        public async Task SendMail(string receiver, string title, string body)
        {
            try
            {
                var email = new MimeMessage
                {
                    Sender = MailboxAddress.Parse(_mailSettings.Email),
                    Subject = title
                };

                email.To.Add(MailboxAddress.Parse(receiver));
                email.From.Add(new MailboxAddress("Pioneers", _mailSettings.Email));

                var builder = new BodyBuilder();
                builder.HtmlBody = body;
                email.Body = builder.ToMessageBody();

                using (var smtp = new SmtpClient())
                {

                    // Connect to the SMTP server using TLS encryption
                    await smtp.ConnectAsync(_mailSettings.Host, _mailSettings.port, SecureSocketOptions.StartTls);

                    // Authenticate with the SMTP server
                    await smtp.AuthenticateAsync(_mailSettings.Email, _mailSettings.Password);

                    // Send the email
                    await smtp.SendAsync(email);

                    // Disconnect from the SMTP server
                    await smtp.DisconnectAsync(true);
                }
            }
            catch (Exception ex)
            {
                // Handle or log the exception
                Console.WriteLine($"An error occurred while sending email: {ex.Message}");
            }
        }
    }
}
