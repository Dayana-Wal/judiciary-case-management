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


    public class SignupService
    {
        private readonly IValidator<SignupModel> _validator = new SignupValidator();

        private readonly IPersonCommandHandler _dataHandler = new PersonCommandHandler();

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

        public async Task<bool> RegisterUser(SignupModel model)
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
                Id = Ulid.NewUlid().ToString(),
                UserName = model.UserName,
                PasswordHash = storedResult.HashedPassword,
                PasswordSalt = storedResult.Salt,
                RoleId = 22,
                PersonId = personId

            };
           
            var addPersonResult =  await _dataHandler.CreatePersonAsync(person);
            if (addPersonResult)
            {
                var addUserResult = await _dataHandler.CreateUserAsync(user);
                if (addUserResult)
                {
                    return true;
                }
            }
            return false;
        }


    }
}
