using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CaseManagement.API.Filters;

namespace CaseManagement.API.Controllers
{
    public class TestController : BaseController
    {
        [CustomAuthorization("Admin")]
        [HttpGet("token")]
        public IActionResult Index()
        {
            return Content("from test");
        }
    }
}
