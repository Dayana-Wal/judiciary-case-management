using CaseManagement.Web.Models;
using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseManagement.Business.Features.Admin
{
    public class UpdateRoleCommand : AbstractCommand
    {
        public string UserId { get; set; } = string.Empty;
        public string RoleName { get; set; } = string.Empty;

        public override ValidationResult Validate()
        {
            var validator = new UpdateUserRoleValidator();
            return validator.Validate(this);
        }
    }

    public class UpdateUserRoleValidator : AbstractValidator<UpdateRoleCommand>
    {
        public UpdateUserRoleValidator()
        {
            RuleFor(x => x.UserId).NotEmpty().WithMessage("UserId cannot be empty.");
            RuleFor(x => x.RoleName).NotEmpty().WithMessage("RoleName cannot be empty.");
        }
    }
}
