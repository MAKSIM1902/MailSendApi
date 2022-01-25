using System.Net.Mail;

namespace ApiSendEmail.Models
{
    public class MailRequest
    {
        public MailAddress Email { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
