using CaseManagement.Business.Common;
using CaseManagement.Business.Service;
using Microsoft.AspNetCore.Mvc;

namespace CaseManagement.API.Controllers
{
    public class JudicialOfficialController : BaseController
    {
        private readonly JudicialOfficialManager _judicialOfficialManager;

        public JudicialOfficialController(JudicialOfficialManager judicialOfficialManager)
        {
            _judicialOfficialManager = judicialOfficialManager;
        }

        [HttpPut("{caseId}/status")]
        public async Task<IActionResult> UpdateCaseStatus(string caseId, [FromQuery] bool isAccepted)
        {
            var result = await _judicialOfficialManager.UpdateCaseStatusAsync(caseId, isAccepted);

            if (result == null)
            {
                // Structured failure response
                var returnResponse = OperationResult<string>.Failed(result.Message);
                return ToResponse(returnResponse);
            }

            // Structured success response
            var successResponse = OperationResult<string>.Success(result.Message);
            return ToResponse(successResponse);
        }


    }
}
