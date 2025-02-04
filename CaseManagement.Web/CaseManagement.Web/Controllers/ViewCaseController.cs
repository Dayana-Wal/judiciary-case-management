using CaseManagement.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace CaseManagement.Web.Controllers
{
    [Route("[controller]")]
    public class ViewCaseController : Controller
    {
        private readonly IConfiguration _configuration;

        public ViewCaseController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet("CaseDetails")]
        public IActionResult CaseView(string caseId)
        {
            var viewCaseObject = new ViewCaseModel();
            var apiBaseUrl = _configuration["ApiSettings:BaseUrl"];
            ViewBag.ApiBaseUrl = apiBaseUrl;
            ViewBag.CaseId = caseId;
            return View(viewCaseObject);
        }
    }
}
