//using CaseManagement.API.Models;
using CaseManagement.Business.Models;
using CaseManagement.Business.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CaseManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SignUpController : Controller
    {
        

        private readonly SignupService _signupService = new SignupService();
        private readonly PasswordService _passwordService;

        public SignUpController()
        {
            _passwordService = new PasswordService();
        }



        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromForm] SignupModel signupDataModel)
        {
            if (signupDataModel == null)
            {
                return BadRequest("Invalid user data.");
            }

            var validationErrors = _signupService.ValidateSignupDetails(signupDataModel);
            if (validationErrors.Any())
            {
                return BadRequest(new { errors = validationErrors });
            }


            //password hashed value and salt value are stored in result
            //var result = _passwordService.UserRegistration(signupDataModel.Password);

            var dataStoreResult = await _signupService.RegisterUser(signupDataModel);
            if ((dataStoreResult))
            {

                return Ok("Validation successful, data stored");
            }
            return BadRequest("Validation successful, data not stored");


        }

    }
}
