using CaseManagement.DataAccess.Entities;
//using CaseManagement.Business.Utility;
using CaseManagement.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace CaseManagement.Web.Controllers
{
    [Route("[Controller]")]
    public class CaseCreationController : Controller
    {
        private readonly CaseManagementContext _caseManagementContext;
        private readonly IConfiguration _configuration;
       // private readonly CacheUtility _cacheUtility;

        public CaseCreationController(CaseManagementContext caseManagementContext, IConfiguration configuration )
        {
            _caseManagementContext = caseManagementContext;
            _configuration = configuration;
           // _cacheUtility = cacheUtility;
        }
        [HttpGet("createcase")]
        public IActionResult CreateCase()
        {
            var caseTypes = _caseManagementContext.LookupConstants
                .Where(c => c.Type == "Case Type")
                .Select(c => c.Text)
                .ToList();

            //var caseTypes = _cacheUtility.GetCaseTypes();

            var model = new CaseCreationModel
            {
                CaseTypes = caseTypes
            };

            var apiBaseUrl = _configuration["ApiSettings:BaseUrl"];
            ViewBag.ApiBaseUrl = apiBaseUrl;

            return View(model);
        }

       

        [HttpGet("CaseCreationSuccess")]
        public IActionResult CaseCreationSuccess()
        {
            return View();
        }
    }
}
