//using CaseManagement.API.Models;
using CaseManagement.Business.Models;
using CaseManagement.Business.Services;
using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CaseManagement.API.Controllers
{
    //[ApiController]
    //[Route("api/[controller]")]
    public class SignUpController : BaseController
    {
        

        private readonly SignupManager _signupManager;
        private readonly PasswordService _passwordService;

        public SignUpController(SignupManager signupManager)
        {
            _passwordService = new PasswordService();
            _signupManager = signupManager;
        }



        [HttpPost("person")]
        public async Task<IActionResult> SignUp([FromBody] SignupModel signupDataModel)
        {
            if (signupDataModel == null)
            {
                return BadRequest("Invalid user data.");
            }

            var validationResult = await _signupManager.ValidateSignupDetails(signupDataModel);
            if (validationResult.Data.Any())
            {
                return BadRequest(new { errors = validationResult });
            }


            //password hashed value and salt value are stored in result
            //var result = _passwordService.UserRegistration(signupDataModel.Password);

            var dataStoreResult = await _signupManager.RegisterUser(signupDataModel);
            return ToResponse(dataStoreResult);


        }

    }
}
