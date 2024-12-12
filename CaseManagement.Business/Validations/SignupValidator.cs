using CaseManagement.Business.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseManagement.Business.Validations
{
    public class SignupValidator : AbstractValidator<SignupModel>
    {
        public SignupValidator() {

            RuleFor(model => model.Name).NotEmpty().WithMessage("Name is required.");

            RuleFor(model => model.UserName).NotEmpty().WithMessage("Username is required.")
                .Length(3, 20).WithMessage("Username length should be between 3 and 20");

            RuleFor(model => model.Password).NotEmpty().WithMessage("Password is required.")
                .Length(7, 20).WithMessage("Password length should be between 7 and 20");

            //RuleFor(model => model.ConfirmPassword).NotEmpty().WithMessage("confirm Password is required.")
            //    .Equal(model => model.Password).WithMessage("Passwords don't match");

            RuleFor(model => model.Email).NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Email is invalid");

            RuleFor(model => model.Contact).NotEmpty().WithMessage("Contact is required.")
                .Matches(@"^\d{10}$").WithMessage("Incorrect number");

            //RuleFor(model => model.AlternateContact).NotEmpty().WithMessage("Alternate contact is required.")
            //.Matches(@"^\d{10}$");

            //RuleFor(model => model.Address).NotEmpty().WithMessage("Address is required.");

            RuleFor(model => model.DateOfBirth).NotEmpty().WithMessage("Date of Birth is required");

            RuleFor(model => model.Gender).NotEmpty().WithMessage("Gender is required.");
        }

    }
}
