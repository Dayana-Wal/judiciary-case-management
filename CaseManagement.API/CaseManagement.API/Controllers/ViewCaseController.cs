using CaseManagement.Business.Common;
using CaseManagement.Business.Service;
using CaseManagement.DataAccess.DTO;
using CaseManagement.DataAccess.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CaseManagement.API.Controllers
{
    public class ViewCaseController : BaseController
    {
        private readonly ViewCaseManager _viewCaseManager;

        public ViewCaseController(ViewCaseManager viewCaseManager)
        {
            _viewCaseManager = viewCaseManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetCaseDetails([FromQuery] string caseId)
        {
            if (string.IsNullOrEmpty(caseId))
            {
                var validationErrors = new List<string> { "Invalid caseId provided." };
                var returnResponse = OperationResult<string>.ValidationError(data: string.Join(", ", validationErrors));
                return ToResponse(returnResponse);
            }
            var caseDetails = await _viewCaseManager.GetCaseDetailsAsync(caseId);
            return ToResponse(caseDetails);
        }

    }
}
