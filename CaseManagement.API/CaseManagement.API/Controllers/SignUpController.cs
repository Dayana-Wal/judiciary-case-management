//using CaseManagement.API.Models;
using CaseManagement.Business.Models;
using CaseManagement.Business.Services;
using CaseManagement.Business.Utility;
using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace CaseManagement.API.Controllers
{

    public class SignupController : BaseController
    {
        

        private readonly SignupManager _signupManager;
        private readonly HashHelper _passwordService;

        public SignupController(SignupManager signupManager, HashHelper passwordservice)
        {
            _passwordService = passwordservice;
            _signupManager = signupManager;
        }



        [HttpPost("person")]
        public async Task<IActionResult> SignUp([FromForm] SignupModel signupDataModel)
        {
            if (signupDataModel == null)
            {
                return BadRequest("Invalid user data.");
            }

            //var validationResult = await _signupManager.ValidateSignupDetails(signupDataModel);
            //if (validationResult.Data.Any())
            //{
            //    return BadRequest(new { errors = validationResult });
            //}


            //password hashed value and salt value are stored in result
            //var result = _passwordService.HashedResult(signupDataModel.Password);

            var dataStoreResult = await _signupManager.RegisterUser(signupDataModel);
            if (dataStoreResult.Status == "Success")
            {
                return Ok(new { status = "Success", message = "User registered successfully!" });
            }
            else
            {
                return BadRequest(new { status = "Failed", message = dataStoreResult.Message });
            }
            //return ToResponse(dataStoreResult);


        }

    }
}
