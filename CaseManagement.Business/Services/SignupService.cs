using CaseManagement.Business.Common;
using CaseManagement.Business.Models;
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


    public class SignupService:BaseManager
    {
        private readonly IValidator<SignupModel> _validator = new SignupValidator();

        private readonly IPersonCommandHandler _dataHandler = new PersonCommandHandler();


        //public SignupService(IValidator<SignupModel> validator)
        //{
        //    _validator = validator;
        //}


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

        public async Task<OperationResult> RegisterUser(SignupModel model)
        {
            PasswordService passwordService = new PasswordService();

            PasswordSaltHashResult storedResult = passwordService.UserRegistration(model.Password);
            
            string personId = Ulid.NewUlid().ToString();
            
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



            var addPersonAndUserResult = await _dataHandler.CreatePersonAndUserAsync(person, user);
            if (addPersonAndUserResult)
            {
                return new OperationResult { Status = "Success", Message = "Person and User details stored successfully!" };
            }
            
            //return addPersonAndUserResult;
            
            return new OperationResult { Status = "Failed" , Message = "Failed to store person and user details"};
        }
        //public async Task<(List<Person> persons, List<User> users)> GetAllPersonsAndUsersAsync()
        //{
        //    return await _dataHandler.GetAllPersonsAndUsersAsync();
        //}


    }
}
