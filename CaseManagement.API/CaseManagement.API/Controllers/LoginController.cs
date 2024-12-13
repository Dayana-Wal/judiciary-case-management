using CaseManagement.API.Models;
using CaseManagement.Business.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace CaseManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : Controller
    {
        private readonly LoginService _loginService;

        public LoginController(LoginService loginService)
        {
           _loginService = loginService;
        }
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest loginRequest)
        {
            Console.WriteLine("Started login method");
            if (!ModelState.IsValid) {
                return BadRequest("Invalid input");
            }
            string token =_loginService.UserLogin(loginRequest.UserName, loginRequest.Password);
            //Call business layer
            Console.WriteLine("Completed login method");
            return Ok($"Login Success: {token}");
        }
    }
}
