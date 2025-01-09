using CaseManagement.Business.Common;
using CaseManagement.Business.Features.Signup;
using CaseManagement.Business.Service;
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
        private readonly EmailBackGroundService _bgService;
        private readonly IPersonCommandHandler _personCommandHandler;
        private readonly EmailService _emailService;

        public SignupController(SignupManager signupManager, HashHelper passwordservice, EmailBackGroundService bgService, IPersonCommandHandler personCommandHandler, EmailService emailService)
        {
            _passwordService = passwordservice;
            _signupManager = signupManager;
            _bgService = bgService;
            _personCommandHandler = personCommandHandler;
            _emailService = emailService;
        }



        [HttpPost("person")]
        public async Task<IActionResult> SignUp([FromBody] SignupCommand signupCommand)
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
                //_bgService.QueueEmail("anudeepthikolagani.999@gmail.com", "Hello", "Hello email");
                await _emailService.SendEmail("anudeepthikolagani.999@gmail.com", "Hello", "Hello email");
                var dataStoreResult = await _signupManager.RegisterUser(signupCommand);

                //signupResult.Status = dataStoreResult.Status;
                //signupResult.Message = dataStoreResult.Message;

                if (dataStoreResult.Status == OperationStatus.Success)
                {
                    signupResult = OperationResult.Success(message: dataStoreResult.Message);
                    //return ToResponse(signupResult);
                }
                else if(dataStoreResult.Status == OperationStatus.Failed)
                {
                    signupResult = OperationResult.Failed(message: dataStoreResult.Message);

                }
                //signupResult = OperationResultConverter.ConvertTo(signupResult, dataStoreResult.Data);

                //var returnResponse = OperationResultConverter.ConvertTo(signupResult, dataStoreResult.Data);
                //TODO: Pass to,subject,body
                _bgService.QueueEmail("dayyubiddika@gmail.com", "Hello", "Hello email");
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
