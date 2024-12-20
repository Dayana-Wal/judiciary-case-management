using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation.Results;

namespace CaseManagement.Business.Features.Otp
{
    public class OtpVerifyCommand
    {
        public string UserId { get; set; } = null!;
        public string Otp { get; set; } = null!;

        public ValidationResult ValidateCommand()
        {
            OtpVerifyCommandValidator validator = new OtpVerifyCommandValidator();
            return validator.Validate(this);
        }
    }

    public class OtpVerifyCommandValidator : AbstractValidator<OtpVerifyCommand>
    {
        public OtpVerifyCommandValidator()
        {
            RuleFor(command => command.UserId)
                .NotEmpty().WithMessage("UserId is required.");

            RuleFor(command => command.Otp)
                .NotEmpty().WithMessage("Otp is required.")
                .Matches(@"^\d{6}$").WithMessage("Otp must be a 6-digit number.");
        }
    }

}
