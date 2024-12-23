using FluentValidation;
using FluentValidation.Results;

namespace CaseManagement.Business.Features.Login
{
    public class LoginQuery
    {
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;

        public ValidationResult ValidateQuery()
        {
            LoginQueryValidator validator = new LoginQueryValidator();
            return validator.Validate(this);
        }

    }

    public class LoginQueryValidator: AbstractValidator<LoginQuery>
    {
        public LoginQueryValidator()
        {
            RuleFor(model => model.UserName).NotEmpty().WithMessage("Username is required.");
            RuleFor(model => model.Password).NotEmpty().WithMessage("Password is required.");
        }
    }
}
