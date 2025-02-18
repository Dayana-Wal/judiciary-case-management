using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseManagement.Business.Features.Case
{
    public class AcceptRejectCaseCommand : AbstractCommand
    {
        public string CaseId { get; set; } = null!;
        public bool IsAccepted { get; set; }

        public override ValidationResult Validate()
        {
            AcceptRejectCaseCommandValidator validator = new AcceptRejectCaseCommandValidator();
            return validator.Validate(this);
        }
    }

    public class AcceptRejectCaseCommandValidator : AbstractValidator<AcceptRejectCaseCommand>
    {
        public AcceptRejectCaseCommandValidator()
        {
            RuleFor(command => command.CaseId)
                .NotEmpty()
                .WithMessage("CaseId shouldn't be empty");

            RuleFor(command => command.IsAccepted)
                .NotNull()
                .WithMessage("IsAccepted shouldn't be null");
        }
    }
}
