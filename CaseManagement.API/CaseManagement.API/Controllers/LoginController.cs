using CaseManagement.Business.Features.Login;
using CaseManagement.Business.Service;
using Microsoft.AspNetCore.Mvc;

namespace CaseManagement.API.Controllers
{
    public class LoginController : BaseController
    {
        private readonly LoginManager _loginManager;

        public LoginController(LoginManager loginManager)
        {
           _loginManager = loginManager;
        }
        [HttpPost("user")]
        public async Task<IActionResult> Login([FromBody] LoginQuery loginQuery)
        {
            try
            {
                if (loginQuery == null) {
                    return BadRequest("Invalid user data");
                }

                var validationResult = loginQuery.ValidateQuery();
                if (validationResult.IsValid)
                {
                    var result = await _loginManager.UserLogin(loginQuery);
                    return ToResponse<string>(result);
                }
                else {
                    //TODO
                    Console.WriteLine("validations failed");
                    return BadRequest("validations failed");
                }
            }
            catch (Exception ex) { 
                return BadRequest(new {Message = "An error occurred while login", Error = ex.Message});
            }
        }
    }
}
