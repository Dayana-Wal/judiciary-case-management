using CaseManagement.DataAccess.Entities;
using CaseManagement.Business.Utility;
using CaseManagement.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace CaseManagement.Web.Controllers
{
    [Route("[Controller]")]
    public class CaseCreationController : Controller
    {
        private readonly CacheUtility _cacheUtility;
        private readonly IConfiguration _configuration;

        public CaseCreationController(CaseManagementContext caseManagementContext, IConfiguration configuration)
        {
            _cacheUtility = new CacheUtility(caseManagementContext); 
            _configuration = configuration;
        }

        [HttpGet("createcase")]
        public IActionResult CreateCase()
        {
            var caseTypes = _cacheUtility.GetCaseTypes();
            var apiBaseUrl = _configuration["ApiSettings:BaseUrl"];
            ViewBag.ApiBaseUrl = apiBaseUrl;
            ViewBag.CaseTypes = caseTypes;

            return View();
        }

        [HttpGet("CaseCreationSuccess")]
        public IActionResult CaseCreationSuccess()
        {
            return View();
        }
    }
}
