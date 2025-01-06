
using FluentValidation;
using FluentValidation.Results;

namespace CaseManagement.Business.Features.Admin
{
    public class UsersQuery
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string SortBy { get; set; } = "Id";
        public string SortDirection { get; set; } = "asc";

        public ValidationResult ValidateCommand()
        {
            UsersQueryValidator validator = new UsersQueryValidator();
            return validator.Validate(this);
        }
    }
    internal class UsersQueryValidator : AbstractValidator<UsersQuery>
    {
        public UsersQueryValidator()
        {
            // PageNumber: Required and must be greater than or equal to 1
            RuleFor(q => q.PageNumber)
                .NotEmpty()
                .WithMessage("Page number is required.")
                .GreaterThanOrEqualTo(1)
                .WithMessage("Page number must be greater than or equal to 1.");

            // PageSize: Optional but must be between 1 and 100 if provided
            RuleFor(q => q.PageSize)
                .Cascade(CascadeMode.Stop)
                .GreaterThanOrEqualTo(1).When(q => q.PageSize != 0)
                .LessThanOrEqualTo(100).When(q => q.PageSize != 0)
                .WithMessage("Page size must be between 1 and 100.");

            // SortDirection: Optional but must be "asc" or "desc" if provided
            RuleFor(q => q.SortDirection)
                .Must(sd => string.IsNullOrEmpty(sd) || sd.ToLower() == "asc" || sd.ToLower() == "desc")
                .WithMessage("Sort direction must be 'asc' or 'desc'.");

            // SortBy: Optional (can add constraints if necessary)
            RuleFor(q => q.SortBy)
                .MaximumLength(50)
                .WithMessage("SortBy field cannot exceed 50 characters.")
                .When(q => !string.IsNullOrEmpty(q.SortBy));
        }
    }

}
