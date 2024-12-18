using CaseManagement.API.Models;
using CaseManagement.Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace CaseManagement.API.Controllers
{
    [AllowAnonymous]
    public class LoginController : BaseController
    {
        private readonly LoginManager _loginManager;

        public LoginController(LoginManager loginManager)
        {
           _loginManager = loginManager;
        }
        [HttpPost("user")]
        public async Task<IActionResult> Login([FromBody] LoginCredentials loginCredentials)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values.SelectMany(value => value.Errors).SelectMany(error => error.ErrorMessage).ToList();
                    return BadRequest(new { Message = "Invalid user input", Error = errors});
                }
                var result = await _loginManager.UserLogin(loginCredentials.UserName, loginCredentials.Password);
                return ToResponse<string>(result);
            }
            catch (Exception ex) { 
                return BadRequest(new {Message = "An error occurred while login", Error = ex.Message});
            }
        }
    }
}
