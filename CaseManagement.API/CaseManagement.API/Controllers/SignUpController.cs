using CaseManagement.API.Models;
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
        private readonly SignupService _signupService;

        public SignUpController()
        {
            _signupService = new SignupService();
        }

        //[HttpGet("id")]
        //public IActionResult SignUp()
        //{
        //    return Ok("success");
        //}

        //[HttpPost("signup")]
        //public async Task<IActionResult> SignUp([FromForm] SignupModel signupModel)
        //{
        //    if (signupModel == null)
        //    {
        //        return BadRequest("Invalid user data.");
        //    }

        //    var validationErrors = _signupService.ValidateSignupDetails(signupModel);
        //    if (validationErrors.Any())
        //    {
        //        return  BadRequest(new { errors = validationErrors });
        //    }

        //    return Ok("validation sucessful");
        //}

        [HttpPost("signup")]
        public IActionResult SignUp([FromForm] SignupModel signupModel)
        {
            if (signupModel == null)
            {
                return BadRequest("Invalid user data.");
            }

            var validationErrors = _signupService.ValidateSignupDetails(signupModel);
            if (validationErrors.Any())
            {
                return BadRequest(new { errors = validationErrors });
            }

            return Ok("Validation successful");
        }

    }
}
