using CaseManagement.Business.Common;
using CaseManagement.Business.Features.Signup;
using CaseManagement.Business.Services;
using CaseManagement.Business.Utility;
using CaseManagement.DataAccess.Commands;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CaseManagement.API.Controllers
{

    public class SignupController : BaseController
    {
        private readonly SignupManager _signupManager;
        private readonly HashHelper _passwordService;
        private readonly IPersonCommandHandler _personCommandHandler;

        public SignupController(SignupManager signupManager, HashHelper passwordservice,  IPersonCommandHandler personCommandHandler)
        {
            _passwordService = passwordservice;
            _signupManager = signupManager;
            _personCommandHandler = personCommandHandler;
        }



        [HttpPost("person")]
        public async Task<IActionResult> SignUp([FromForm] SignupCommand signupCommand)
        {
            if (signupCommand == null)
            {
                return BadRequest("Invalid user data.");
            }

            var validationResult = signupCommand.ValidateCommand();
            var signupResult = new OperationResult();

            //var signupResult = new OperationResult();


            if (validationResult.IsValid)
            {
                var dataStoreResult = await _signupManager.RegisterUser(signupCommand);

                //signupResult.Status = dataStoreResult.Status;
                //signupResult.Message = dataStoreResult.Message;

                if (dataStoreResult.Status == "Success")
                {
                    signupResult = OperationResult.Success(message: dataStoreResult.Message);
                    //return ToResponse(signupResult);
                }
                else if(dataStoreResult.Status == "Failed")
                {
                    signupResult = OperationResult.Failed(message: dataStoreResult.Message);

                }
                //signupResult = OperationResultConverter.ConvertTo(signupResult, dataStoreResult.Data);

                //var returnResponse = OperationResultConverter.ConvertTo(signupResult, dataStoreResult.Data);

                return ToResponse(signupResult);


            }
            else
            {
                var validationErrors = new List<string>();

                foreach(var errors in validationResult.Errors)
                {
                    validationErrors.Add(errors.ErrorMessage);
                }

                //signupResult.Status = "Failed";
                //signupResult.Message = "Registration Failed";

                //var returnResponse = OperationResultConverter.ConvertTo(signupResult, validationErrors);

                //signupResult = OperationResultT<List<string>>.ValidationError(validationErrors);
                
                var returnResponse = OperationResult<List<string>>.ValidationError(data: validationErrors);

                return ToResponse(returnResponse);
                
            }

        }

    }
}
