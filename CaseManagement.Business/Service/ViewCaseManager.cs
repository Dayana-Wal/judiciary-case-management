using CaseManagement.Business.Common;
using CaseManagement.Business.Queries;
using CaseManagement.DataAccess.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CaseManagement.Business.Service
{
    public class ViewCaseManager : BaseManager
    {
        private readonly IViewCaseQueryHandler _viewCaseQueryHandler;

        public ViewCaseManager(IViewCaseQueryHandler viewCaseQueryHandler)
        {
            _viewCaseQueryHandler = viewCaseQueryHandler;
        }

        public async Task<OperationResult<CaseDetailsDto>> GetCaseDetailsAsync(string caseId)
        {
            var caseDetails = await _viewCaseQueryHandler.GetCaseDetailsAsync(caseId);
            if (caseDetails == null)
            {
                return OperationResult<CaseDetailsDto>.Failed(data: null, message: "Case not found.");
            }
            return OperationResult<CaseDetailsDto>.Success(message: "Case details retrieved successfully.", data: caseDetails);
        }
    }
}
