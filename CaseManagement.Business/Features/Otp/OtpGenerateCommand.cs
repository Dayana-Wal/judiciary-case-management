using FluentValidation;
using FluentValidation.Results;

namespace CaseManagement.Business.Features.Otp
{
    public class OtpGenerateCommand
    {

        public string PhoneNumber { get; set; } = null!;
        public string UsedForCode { get; set; } = null!;
        public string UserId { get; set; } = null!;

        public ValidationResult ValidateCommand()
        {
            OtpGenerateCommandValidator validator = new OtpGenerateCommandValidator();
            return validator.Validate(this);
        }
    }

    public class OtpGenerateCommandValidator : AbstractValidator<OtpGenerateCommand>
    {
        public OtpGenerateCommandValidator()
        {
            RuleFor(command => command.UsedForCode)
                .NotEmpty().WithMessage("OtpCode is required.");

            RuleFor(command => command.PhoneNumber)
              .Matches(@"^\+?[1-9]\d{1,14}$")
              .WithMessage("Phone Number is invalid.");

            RuleFor(command => command.UserId)
             .NotEmpty().WithMessage("RequestedBy is required.");
        }
}
}
