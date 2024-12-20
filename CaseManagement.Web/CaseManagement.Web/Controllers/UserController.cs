using CaseManagement.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace CaseManagement.Web.Controllers
{
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly IConfiguration _configuration;

        public UserController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        //Display the form
        [HttpGet("signup")]
        public IActionResult SignUp()
        {
            var apiBaseUrl = _configuration["ApiSettings:BaseUrl"];
            ViewBag.ApiBaseUrl = apiBaseUrl;
            return View();
        }

        [HttpGet("create-case")]
        public IActionResult CreateCase()
        {
            return View();
        }
    }
}
