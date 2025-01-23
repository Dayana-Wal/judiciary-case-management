using FluentValidation;
using FluentValidation.Results;

namespace CaseManagement.Business.Features.Case
{
    public class CreateCaseCommand: AbstractCommand
    {
        public string CaseType { get; set; } = null!;
        public string VictimName { get; set; } = null!;
        public long VictimContact { get; set; } = 0!;
        public string AccusedName { get; set; } = null!;
        public long AccusedContact {  get; set; } = 0!;
        public DateTime? DateOfIncident { get; set; }
        public string Description { get; set; } = null!;
        public List<string> FileIds { get; set; }         

        public override ValidationResult Validate()
        {
            CreateCaseCommandValidator validator = new CreateCaseCommandValidator();
            return validator.Validate(this);
        }
    }
    public class CreateCaseCommandValidator : AbstractValidator<CreateCaseCommand>
    {
        public CreateCaseCommandValidator()
        {
            RuleFor(model => model.CaseType).NotEmpty().WithMessage("Case type field is required");

            RuleFor(model => model.VictimName).NotEmpty().WithMessage("Victim Name field is required");

            RuleFor(model => model.AccusedName).NotEmpty().WithMessage("Accused Name field is required");

            RuleFor(model => model.DateOfIncident).NotEmpty().WithMessage("Incident Date field is required");

            RuleFor(model => model.VictimContact).NotEmpty().WithMessage("Victim Contact is required.");

            RuleFor(model => model.AccusedContact).NotEmpty().WithMessage("Accused Contact is required.");

            RuleFor(model => model.Description).NotEmpty().WithMessage("Description field is required")
                .MinimumLength(10).WithMessage("Description should be at least 50 characters.");
        }
    }
}
