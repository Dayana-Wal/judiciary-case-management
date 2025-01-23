using FluentValidation.Results;
using System;
using System.Collections.Generic;
using FluentValidation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation.Results;

namespace CaseManagement.Business.Utility
{
    public static class Extension
    {
        public static List<String> GetErrors(this ValidationResult validationResult)
        {
            var validationErrors = new List<string>();

            foreach (var errors in validationResult.Errors)
            {
                validationErrors.Add(errors.ErrorMessage);
            }

            return validationErrors;
        }
    }
}
