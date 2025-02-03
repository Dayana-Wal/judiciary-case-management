using Microsoft.AspNetCore.Mvc;

namespace CaseManagement.Web.Controllers
{
    [Route("[controller]")]
    public class AdvocateController : BaseController
    {
        public AdvocateController(IConfiguration configuration) : base(configuration)
        {
        }

        [HttpGet("details")]
        public IActionResult AdvocateDetails()
        {
            ViewBag.ApiBaseUrl = GetApiBaseUrl();
            return View();
        }
    }
}
