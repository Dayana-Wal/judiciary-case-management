using CaseManagement.Business.Service;
using Microsoft.AspNetCore.Mvc;

namespace CaseManagement.API.Controllers
{
    public class AdvocateController : BaseController
    {
        private readonly AdvocateManager _advocateManager;
        public AdvocateController(AdvocateManager advocateManager)
        {
            _advocateManager = advocateManager;
        }
        [HttpGet("view")]
        public async Task<IActionResult> GetAdvocates()
        {
            var response = await _advocateManager.GetAdvocateDetails();
            return ToResponse(response);
        }
    }
}
