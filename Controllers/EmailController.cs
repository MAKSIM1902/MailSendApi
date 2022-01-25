using ApiSendEmail.Interfaces;
using ApiSendEmail.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;
using ApiSendEmail.Attributes;
using Microsoft.Extensions.Logging;

namespace ApiSendEmail.Controllers
{
    [Route("api/sendEmail")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IMailService _mailService;
        private readonly ILogger _logger; 
        public EmailController(IMailService mailService, ILogger<EmailController> logger)
        {
            _mailService = mailService;
            _logger = logger;
        }
        [ApiKey]
        [HttpPost]
        public async Task<IActionResult> Send([FromBody] MailRequest request, CancellationToken cancelToken)
        {
            _logger.LogWarning($"Email: {request.Email}, Subject: {request.Body}, Body: {request.Body}");
            await _mailService.SendEmailAsync(request, cancelToken);

            return Ok();
        }
    }
}
