using CaseManagement.Business.Commands;
using CaseManagement.Business.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseManagement.Business.Service
{
    public class JudicialOfficialManager
    {
        private readonly ICaseAcceptRejectCommandHandler _caseAcceptRejectCommandHandler;

        public JudicialOfficialManager(ICaseAcceptRejectCommandHandler caseAcceptRejectCommandHandler)
        {
            _caseAcceptRejectCommandHandler = caseAcceptRejectCommandHandler;
        }

        public async Task<OperationResult> UpdateCaseStatusAsync(string caseId, bool isAccepted)
        {
            var action = isAccepted ? "Accept" : "Reject";
            return await _caseAcceptRejectCommandHandler.TakeCaseActionAsync(caseId, action);
        }
    }
}
