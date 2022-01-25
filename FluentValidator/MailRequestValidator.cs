using ApiSendEmail.Models;
using FluentValidation;

namespace ApiSendEmail.FluentValidator
{
    public class MailRequestValidator : AbstractValidator<MailRequest>
    {
        public MailRequestValidator()
        {
            //RuleFor(m => m.Email).NotEmpty().EmailAddress();
            RuleFor(m => m.Subject).MaximumLength(100);
            RuleFor(m => m.Body).NotEmpty().MaximumLength(1000);
        }
    }
}