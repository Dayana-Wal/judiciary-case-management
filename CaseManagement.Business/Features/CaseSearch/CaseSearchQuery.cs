using FluentValidation;
using FluentValidation.Results;

namespace CaseManagement.Web.Models
{
    public class CaseSearchQuery
    {
        public string? SearchCategory { get; set; }
        public string? SearchValue { get; set; }

        public ValidationResult ValidateSearchModel()
        {
            var validator = new SearchModelValidator();
            return validator.Validate(this);
        }
    }

    public class SearchModelValidator : AbstractValidator<CaseSearchQuery>
    {
        public SearchModelValidator()
        {
            RuleFor(model => model.SearchCategory)
                .NotEmpty().WithMessage("Search category is required.")
                .Must(category => new[]
                {
                    "CaseNumber", "VictimName", "AccusedName", "AdvocateName",
                    "EmailAddress", "PhoneNumber"
                }.Contains(category))
                .WithMessage("Invalid search category.");

            RuleFor(model => model.SearchValue)
                .NotEmpty().WithMessage("Search value is required.");
        }
    }
}
