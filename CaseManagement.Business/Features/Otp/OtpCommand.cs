using FluentValidation;
using FluentValidation.Results;

namespace CaseManagement.Business.Features.Otp
{
    public class OtpCommand
    {
        public string UserId { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string UsedForCode { get; set; } = null!;

        public ValidationResult ValidateCommand()
        {
            OtpCommandValidator validator = new OtpCommandValidator();
            return validator.Validate(this);
        }
    }

    public class OtpCommandValidator : AbstractValidator<OtpCommand>
    {
        public OtpCommandValidator()
        {
            RuleFor(command => command.UserId)
                .NotEmpty().WithMessage("UserId is required.");

            RuleFor(command => command.UsedForCode)
                .NotEmpty().WithMessage("OtpCode is required.");

            RuleFor(command => command.PhoneNumber)
              .Matches(@"^\+?[1-9]\d{1,14}$")
              .WithMessage("Phone Number is invalid.");
        }
}
}
