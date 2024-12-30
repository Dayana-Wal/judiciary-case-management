using CaseManagement.Web.Models;
using CaseManagement.DataAccess.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Threading.Tasks;
using System.Linq;

namespace CaseManagement.Web.Controllers
{
    [Route("[Controller]")]
    public class CaseCreationController : Controller
    {
        private readonly CaseManagementContext _caseManagementContext;
        private readonly IConfiguration _configuration;

        public CaseCreationController(CaseManagementContext caseManagementContext, IConfiguration configuration)
        {
            _caseManagementContext = caseManagementContext;
            _configuration = configuration;
        }
        [HttpGet("createcase")]
        public IActionResult CreateCase()
        {
            var caseTypes = _caseManagementContext.LookupConstants
                .Where(c => c.Type == "Case Type")
                .Select(c => c.Text)
                .ToList();

            var model = new CaseCreationModel
            {
                CaseTypes = caseTypes
            };

            return View(model);
        }

        //[HttpPost("createcase")]
        //public async Task<IActionResult> CreateCase(CaseCreationModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        // Save files to the server if there are any
        //        if (model.CaseFiles != null && model.CaseFiles.Count > 0)
        //        {
        //            foreach (var file in model.CaseFiles)
        //            {
        //                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", file.FileName);
        //                using (var stream = new FileStream(filePath, FileMode.Create))
        //                {
        //                    await file.CopyToAsync(stream);
        //                }
        //            }
        //        }

        //        // Optionally, save case details in the database or perform other business logic
        //        // _caseManagementContext.Cases.Add(new Case { ... });
        //        // await _caseManagementContext.SaveChangesAsync();

        //        // Redirect to a confirmation page or success page
        //        return RedirectToAction("CaseCreationSuccess");
        //    }

        //    // If the model is not valid, redisplay the form with validation messages
        //    return View(model);
        //}

        [HttpGet("CaseCreationSuccess")]
        public IActionResult CaseCreationSuccess()
        {
            return View();
        }
    }
}
