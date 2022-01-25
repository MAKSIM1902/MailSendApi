using System.Threading;
using ApiSendEmail.Models;
using System.Threading.Tasks;

namespace ApiSendEmail.Interfaces
{
    public interface IMailService
    {   
       public Task SendEmailAsync(MailRequest mailRequest, CancellationToken cancelToken);
    }
}
