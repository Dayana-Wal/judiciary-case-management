using CaseManagement.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace CaseManagement.Web.Controllers
{
    [Route("[controller]")]
    public class UserController : BaseController
    {
        public UserController(IConfiguration configuration) : base(configuration)
        {
        }

        //Display the form
        [HttpGet("signup")]
        public IActionResult Signup()
        {
            ViewBag.ApiBaseUrl = GetApiBaseUrl();
            return View();
        }

        [HttpGet("login")]
        public IActionResult Login()
        {
            ViewBag.ApiBaseUrl = GetApiBaseUrl();
            return View();
        }
    }
}
