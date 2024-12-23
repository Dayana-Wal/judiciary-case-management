using FluentValidation;
using FluentValidation.Results;

namespace CaseManagement.Business.Features.Signup
{
    public class SignupCommand
    {
        public string Name { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Contact { get; set; } = null!;
        public DateTime? DateOfBirth { get; set; } 
        public string Gender { get; set; } = null!;

        public ValidationResult ValidateCommand() { 
            SignupCommandValidator validator = new SignupCommandValidator();
            return validator.Validate(this);
        }
    }

    public class SignupCommandValidator : AbstractValidator<SignupCommand>
    {
        public SignupCommandValidator()
        {

            RuleFor(model => model.Name).NotEmpty().WithMessage("Name is required.");

            RuleFor(model => model.UserName).NotEmpty().WithMessage("Username is required.")
                .Length(3, 20).WithMessage("Username length should be between 3 and 20");

            RuleFor(model => model.Password).NotEmpty().WithMessage("Password is required.")
                .Length(7, 20).WithMessage("Password length should be between 7 and 20");

            RuleFor(model => model.Email).NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Email is invalid");

            RuleFor(model => model.Contact).NotEmpty().WithMessage("Contact is required.")
                .Matches(@"^\d{10}$").WithMessage("Incorrect number");

            RuleFor(model => model.DateOfBirth).NotEmpty().WithMessage("Date of Birth is required");

            RuleFor(model => model.Gender).NotEmpty().WithMessage("Gender is required.");
        }

    }
}
