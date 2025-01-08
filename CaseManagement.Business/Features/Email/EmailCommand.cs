using CaseManagement.Web.Models;
using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseManagement.Business.Features.Email
{
    public class EmailCommand : AbstractCommand
    {
        public string To { get; set; }
        public string Body { get; set; }
        public string Subject { get; set; }

        public override ValidationResult Validate()
        {
            var validator = new EmailCommandValidator();
            return validator.Validate(this);
        }

        public class EmailCommandValidator : AbstractValidator<EmailCommand>
        {
            public EmailCommandValidator()
            {
                RuleFor(model => model.To)
                    .NotEmpty().WithMessage("To is required.");
                RuleFor(model => model.Subject)
                    .NotEmpty().WithMessage("Subject is required.");
                RuleFor(model => model.Body)
                    .NotEmpty().WithMessage("Body is required.");
            }
        }
    }
}
