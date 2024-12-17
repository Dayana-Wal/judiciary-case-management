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
        private readonly LoginManager _loginManager;

        public LoginController(LoginManager loginManager)
        {
           _loginManager = loginManager;
        }
        [HttpPost("user")]
        public IActionResult Login([FromBody] LoginRequest loginRequest)
        {
            Console.WriteLine("started");
           if (!ModelState.IsValid) {
                return BadRequest("Invalid input");
            }
            var result =_loginManager.UserLogin(loginRequest.UserName, loginRequest.Password);
            //Call business layer
            return Ok($"Login Success: {result}");
        }
    }
}
