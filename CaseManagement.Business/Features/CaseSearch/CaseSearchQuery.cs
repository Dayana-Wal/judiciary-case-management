using CaseManagement.Business.Features;
using FluentValidation;
using FluentValidation.Results;

namespace CaseManagement.Web.Models
{
    public class CaseSearchQuery : AbstractQuery
    {
        public string? SearchCategory { get; set; }
        public string? SearchValue { get; set; }

        public override ValidationResult Validate()
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
                .Must(category => string.IsNullOrEmpty(category) || new[] {
                "CaseNumber", "VictimName", "AccusedName", "AdvocateName", "EmailAddress", "PhoneNumber"
                }.Contains(category))
                .WithMessage("Invalid search category.");

            RuleFor(model => model.SearchValue)
                .Must(value => string.IsNullOrEmpty(value) || !string.IsNullOrWhiteSpace(value))
                .WithMessage("Search value is required.");
        }
    }
}
