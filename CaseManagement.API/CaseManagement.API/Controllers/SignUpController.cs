using CaseManagement.Business.Common;
using CaseManagement.Business.Features.Signup;
using CaseManagement.Business.Services;
using CaseManagement.Business.Utility;
using CaseManagement.DataAccess.Commands;
using Microsoft.AspNetCore.Mvc;

namespace CaseManagement.API.Controllers
{
    //[ApiController]
    //[Route("api/[controller]")]
    public class SignUpController : BaseController
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

            var validationresult = signupCommand.ValidateCommand();

            var signupResult = new OperationResult();


            if(validationresult.IsValid)
            {
                var dataStoreResult = await _signupManager.RegisterUser(signupCommand);

                signupResult.Status = dataStoreResult.Status;
                signupResult.Message = dataStoreResult.Message;

                if (dataStoreResult.Status == "Success")
                {
                    
                    return ToResponse(signupResult);
                }
                else
                {
                    var returnResponse = OperationResultConverter.ConvertTo(signupResult, dataStoreResult.Data);

                    return ToResponse(returnResponse);
                }

            }
            else
            {
                List<string> validationErrors = new List<string>();

                foreach(var errors in validationresult.Errors)
                {
                    validationErrors.Add(errors.ErrorMessage);
                }

                signupResult.Status = "Failed";
                signupResult.Message = "Registration Failed";
                
                var returnResponse = OperationResultConverter.ConvertTo(signupResult, validationErrors);
                return ToResponse(returnResponse);
                
            }


           


        }

    }
}
