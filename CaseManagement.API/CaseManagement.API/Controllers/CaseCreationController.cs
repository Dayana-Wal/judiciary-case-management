using CaseManagement.Business.Commands;
using CaseManagement.Business.Common;
using CaseManagement.Business.Features.Case;
using CaseManagement.Business.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CaseManagement.API.Controllers
{
    public class CaseCreationController : BaseController
    {
        private readonly CaseCreationManager _caseCreationManager;
        private readonly ICaseCommandHandler _caseCommandHandler;

        public CaseCreationController(CaseCreationManager caseCreationManager, ICaseCommandHandler caseCommandHandler)
        {
            _caseCreationManager = caseCreationManager;
            _caseCommandHandler = caseCommandHandler;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCase([FromBody] CreateCaseCommand createCaseCommand)
        {
            if (createCaseCommand == null)
            {
                return BadRequest("Invalid user data");
            }

            var validationResult = createCaseCommand.ValidationCommand();
            var caseCreationResult = new OperationResult();

            if (validationResult.IsValid)
            {
                var dataStoreResult = await _caseCreationManager.CreateCase(createCaseCommand);
                if (dataStoreResult.Status == OperationStatus.Success)
                {
                    caseCreationResult = OperationResult.Success(message: dataStoreResult.Message);

                }
                else if (dataStoreResult.Status == OperationStatus.Failed)
                {
                    caseCreationResult = OperationResult.Failed(message: dataStoreResult.Message);

                }

                return ToResponse(caseCreationResult);

            }
            else
            {
                var validationErrors = new List<string>();

                foreach (var errors in validationResult.Errors)
                {
                    validationErrors.Add(errors.ErrorMessage);
                }

                var returnResponse = OperationResult<List<string>>.ValidationError(data: validationErrors);

                return ToResponse(returnResponse);

            }
        }
    }
            
}
