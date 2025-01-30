using FluentValidation;
using FluentValidation.Results;

namespace CaseManagement.Business.Features.Case
{
    public class AssignAdvocateCommand : AbstractCommand
    {
        public string CaseId { get; set; } = null!;
        public string AdvocateId { get; set; } = null!;
        public override ValidationResult Validate()
        {
            AssignAdvocateCommandValidator validator = new AssignAdvocateCommandValidator();
            return validator.Validate(this);
        }
    }

    public class AssignAdvocateCommandValidator : AbstractValidator<AssignAdvocateCommand>
    {
        public AssignAdvocateCommandValidator()
        {
            RuleFor(model => model.CaseId).NotEmpty().WithMessage("CaseId shouldn't be empty");
            RuleFor(model => model.AdvocateId).NotEmpty().WithMessage("CaseId shouldn't be empty");

        }
    }
}
