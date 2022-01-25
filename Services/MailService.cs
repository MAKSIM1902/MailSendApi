using System.Threading;
using ApiSendEmail.Interfaces;
using ApiSendEmail.Models;
using ApiSendEmail.Settings;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using System.Threading.Tasks;

namespace ApiSendEmail.Services
{
    public class MailService : IMailService
    {
        private readonly MailSettings _mailSettings;
        public MailService(IOptions<MailSettings> mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }
        public async Task SendEmailAsync(MailRequest mailRequest, CancellationToken cancelToken)
        {
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_mailSettings.Mail.Address);
            email.To.Add(MailboxAddress.Parse(mailRequest.Email.Address));
            email.Subject = mailRequest.Subject;
            var builder = new BodyBuilder();
            builder.HtmlBody = mailRequest.Body;
            email.Body = builder.ToMessageBody();
            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls, cancelToken);
            await smtp.AuthenticateAsync(_mailSettings.Mail.Address, _mailSettings.Password, cancelToken);
            await smtp.SendAsync(email, cancelToken);
            await smtp.DisconnectAsync(true, cancelToken);
        }
    }
}
