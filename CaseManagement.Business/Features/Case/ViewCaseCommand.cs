using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseManagement.Business.Features.Case
{
    public class ViewCaseCommand : AbstractCommand
    {
        public string CaseId { get; set; } = null!;

        public override ValidationResult Validate()
        {
            var validator = new ViewCaseCommandValidator();
            return validator.Validate(this);
        }
    }

    public class ViewCaseCommandValidator : AbstractValidator<ViewCaseCommand>
    {
        public ViewCaseCommandValidator()
        {
            RuleFor(model => model.CaseId)
                .NotEmpty()
                .WithMessage("CaseId shouldn't be empty");
        }
    }
}
