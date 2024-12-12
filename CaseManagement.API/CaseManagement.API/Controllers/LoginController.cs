using CaseManagement.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace CaseManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : Controller
    {
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest loginRequest)
        {
            if (!ModelState.IsValid) {
                return BadRequest("Invalid input");
            }
            //Call business layer

            return View();
        }
    }
}
