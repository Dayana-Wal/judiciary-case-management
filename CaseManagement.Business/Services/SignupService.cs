﻿using CaseManagement.Business.Models;
using CaseManagement.Business.Validations;
using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseManagement.Business.Services
{

    public class SignupService
    {
        private readonly IValidator<SignupModel> _validator = new SignupValidator();


        //public SignupService(IValidator<SignupModel> validator)
        //{
        //    _validator = validator;
        //}


        public List<string> ValidateSignupDetails(SignupModel userDataModel)
        {            
            List<string> validationErrors = new List<string>();

            var validation = _validator.Validate(userDataModel);

            if (!(validation.IsValid))
            {

                foreach (var error in validation.Errors) {
                    validationErrors.Add(error.ErrorMessage); 
                }
                
            }
            return validationErrors;

        }
    }
}