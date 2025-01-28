using CaseManagement.Business.Commands;
using CaseManagement.Business.Common;
using CaseManagement.Business.Features.Case;
using CaseManagement.Business.Service;
using CaseManagement.Business.Utility;
using Microsoft.AspNetCore.Mvc;

namespace CaseManagement.API.Controllers
{
    public class CaseController : BaseController
    {
        private readonly CaseManager _caseManager;
        private readonly ICaseCommandHandler _caseCommandHandler;

        public CaseController(CaseManager caseManager, ICaseCommandHandler caseCommandHandler)
        {
            _caseManager = caseManager;
            _caseCommandHandler = caseCommandHandler;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateCase([FromBody] CreateCaseCommand createCaseCommand)
        {
            
            if (createCaseCommand == null)
            {
                return BadRequest("Invalid user data");
            }

            var validationResult = createCaseCommand.Validate();
            var caseCreationResult = new OperationResult();

            if (validationResult.IsValid)
            {
                var dataStoreResult = await _caseManager.RegisterCase(createCaseCommand);
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
                var validationErrors = Extension.GetErrors(validationResult);
              
                var returnResponse = OperationResult<List<string>>.ValidationError(data: validationErrors);

                return ToResponse(returnResponse);

            }
        }

        [HttpPost("assign-advocate")]
        public async Task<IActionResult> AssignAdvocate([FromBody] AssignAdvocateCommand assignAdvocateCommand)
        {
            if(assignAdvocateCommand is null)
            {
                return BadRequest("Invalid Data");
            }

            var validationResult = assignAdvocateCommand.Validate();
            if (!validationResult.IsValid)
            {
                var validationErrors = Extension.GetErrors(validationResult);
                var res = OperationResult<List<string>>.ValidationError(data: validationErrors);
                return ToResponse(res);
            }

            var response = await _caseManager.AssignAdvocate(assignAdvocateCommand);
            return ToResponse(response);
        }
    }
            
}
