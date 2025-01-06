using Microsoft.AspNetCore.Mvc;

namespace CaseManagement.Web.Controllers
{
    [Route("[controller]")]
    public class CaseSearchController : Controller
    {
        private readonly IConfiguration _configuration;

        public CaseSearchController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // Display the search form
        [HttpGet("search")]
        public IActionResult Search()
        {
            var apiBaseUrl = _configuration["ApiSettings:BaseUrl"];
            ViewBag.ApiBaseUrl = apiBaseUrl;
            return View();
        }

    }
}
