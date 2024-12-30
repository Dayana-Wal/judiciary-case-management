using FluentValidation;
using FluentValidation.Results;

using Microsoft.AspNetCore.Http;
namespace CaseManagement.Business.Features.Case
{
    public class CreateCaseCommand
    {
        public string CaseType { get; set; } = null!;
        public string VictimName { get; set; } = null!;
        public int VictimContact { get; set; } = 0!;
        public string AccusedName { get; set; } = null!;
        public DateTime DateOfIncident { get; set; }
        //public string IncidentLocation { get; set; } = null!;

        //public List<IFormFile> CaseFiles { get; set; } = new List<IFormFile>();
        public string Description { get; set; } = null!;

        public List<string> CaseTypes { get; set; } = new List<string>();
        public bool Confirm { get; set; }

        public ValidationResult ValidationCommand()
        {
            CreateCaseCommandValidator validator = new CreateCaseCommandValidator();
            return validator.Validate(this);
        }
    }
    public class CreateCaseCommandValidator : AbstractValidator<CreateCaseCommand>
    {
        public CreateCaseCommandValidator()
        {
            RuleFor(model => model.CaseType).NotEmpty().WithMessage("Case type is required");

            RuleFor(model => model.VictimName).NotEmpty().WithMessage("Victim Name is required");

            RuleFor(model => model.AccusedName).NotEmpty().WithMessage("Accused Name is required");

            RuleFor(model => model.DateOfIncident).NotEmpty().WithMessage("Incident Date is required");

            //RuleFor(model => model.IncidentLocation).NotEmpty().WithMessage("Incident Location is required");

            //RuleFor(model => model.CaseFiles).NotEmpty().WithMessage("Case File(s) are required");

            RuleFor(model => model.Description).NotEmpty().WithMessage("Description is required")
                .MinimumLength(50).WithMessage("Minimum 50 characters required");

            RuleFor(model => model.Confirm).NotEmpty().WithMessage("Checkbox is required");


        }
    }
}
