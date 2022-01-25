using System.Configuration;
using System.Net.Mail;
using System.Text.Json;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace ApiSendEmail.Settings
{
    public class MailSettings
    {
        public MailAddress Mail { get; set; }
        public string Password { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
    }
}
