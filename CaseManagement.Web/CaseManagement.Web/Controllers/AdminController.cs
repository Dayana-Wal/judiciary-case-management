using Microsoft.AspNetCore.Mvc;

namespace CaseManagement.Web.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
