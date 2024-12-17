using CaseManagement.Business.Common;
using CaseManagement.Business.Models;
using CaseManagement.Business.Utility;
using CaseManagement.Business.Validations;
using CaseManagement.DataAccess.Commands;
using CaseManagement.DataAccess.Entities;
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


    public class SignupManager:BaseManager
    {
        private readonly IValidator<SignupModel> _validator;

        private readonly IPersonCommandHandler _dataHandler;


        public SignupManager(IPersonCommandHandler dataHandler, SignupValidator validator)
        {
            //_validator = new SignupValidator();
            _validator = validator;
            _dataHandler = dataHandler;
        }


        public async Task<OperationResult<List<string>>> ValidateSignupDetails(SignupModel userDataModel)
        {            
            List<string> validationErrors = new List<string>();

            var validation = _validator.Validate(userDataModel);

            if (!(validation.IsValid))
            {

                foreach (var error in validation.Errors) {
                    validationErrors.Add(error.ErrorMessage); 
                }
                
            }

            OperationResult<List<string>> validationResult = new OperationResult<List<string>>
            {
                Data = validationErrors
            };
            if (validationResult.Data.Any())
            {
                validationResult.Status = "Failed";
                validationResult.Message = "Fluent Validation executed, Invalid data - please check the entered details ";
            }
            else
            {
                validationResult.Status = "Success";
                validationResult.Message = "Fluent Validation executed and passed the rules";
            }
            return validationResult;

        }

        public async Task<OperationResult<List<string>>> RegisterUser(SignupModel model)
        {
            var validationResult = await ValidateSignupDetails(model);
            if (validationResult.Data.Any())
            {
                return validationResult;
                //return new OperationResult<List<string>>
                //{
                //    Status = "Failed",
                //    Message = "Validation failed: Please check the entered details.",
                //    Data = validationResult.Data
                //};
            }

            HashHelper passwordService = new HashHelper();

            PasswordSaltHashResult storedResult = passwordService.HashedResult(model.Password);

            string personId = NewUlid();
            
            var person = new Person
            {
                Id = personId,
                Name = model.Name,

                Email = model.Email,
                Contact = Convert.ToInt64(model.Contact),
                DateOfBirth = Convert.ToDateTime(model.DateOfBirth),
                Gender = model.Gender
            };

            var user = new User
            {
                Id = NewUlid(),
                UserName = model.UserName,
                PasswordHash = storedResult.HashedPassword,
                PasswordSalt = storedResult.Salt,
                RoleId = 22,
                PersonId = personId

            };


            //var addPersonResult =  await _dataHandler.CreatePersonAsync(person);
            //if (addPersonResult)
            //{
            //    var addUserResult = await _dataHandler.CreateUserAsync(user);
            //    if (addUserResult)
            //    {
            //        OperationResult result = new OperationResult
            //        {
            //            Status = "Success",
            //            Message = "Person and user are successfully added"
            //        };

            //        return result;

            //    }
            //    else { }
            //}

            var addPersonAndUserResult = await _dataHandler.CreateUserAsync(person, user);
            if (addPersonAndUserResult == "Success")
            {
                return new OperationResult<List<string>> { Status = "Success", Message = "Person and User details stored successfully!" };
            }
            else
            {
                return new OperationResult<List<string>> { Status = "Failed", Message = $"Failed to store person and user details:{addPersonAndUserResult}" };

            }

        }


    }
}
