//using CaseManagement.API.Models;
using CaseManagement.Business.Models;
using CaseManagement.Business.Services;
using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CaseManagement.API.Controllers
{
    //[ApiController]
    //[Route("api/[controller]")]
    [AllowAnonymous]
    public class SignUpController : BaseController
    {
        

        private readonly SignupService _signupService = new SignupService();
        private readonly PasswordService _passwordService;

        public SignUpController()
        {
            _passwordService = new PasswordService();
        }



        [HttpPost("person")]
        public async Task<IActionResult> SignUp([FromForm] SignupModel signupDataModel)
        {
            if (signupDataModel == null)
            {
                return BadRequest("Invalid user data.");
            }

            var validationResult = await _signupService.ValidateSignupDetails(signupDataModel);
            if (validationResult.Data.Any())
            {
                return BadRequest(new { errors = validationResult });
            }


            //password hashed value and salt value are stored in result
            //var result = _passwordService.UserRegistration(signupDataModel.Password);

            var dataStoreResult = await _signupService.RegisterUser(signupDataModel);
            return ToResponse(dataStoreResult);


        }

    }
}
