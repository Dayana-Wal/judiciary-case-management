using CaseManagement.Business.Common;
using CaseManagement.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseManagement.Business.Commands
{
    public class CaseAcceptRejectCommandHandler : ICaseAcceptRejectCommandHandler
    {
        private readonly CaseManagementContext _context;

        public CaseAcceptRejectCommandHandler(CaseManagementContext context)
        {
            _context = context;
        }

        public async Task<OperationResult> TakeCaseActionAsync(string caseId, string action)
        {
            if (string.IsNullOrWhiteSpace(action))
                return OperationResult.Failed("Action cannot be null or empty.");

            var caseEntity = await _context.Cases
                .Include(c => c.CaseStatus)
                .FirstOrDefaultAsync(c => c.Id == caseId.ToString());

            if (caseEntity == null)
                return OperationResult.Failed("Case not found.");

            var targetStatusCode = action == "Accept" ? "IPE" : "REJ";
            var targetCaseStatus = await _context.LookupConstants
                .FirstOrDefaultAsync(lc => lc.Code == targetStatusCode && lc.Type == "Case Status");

            if (targetCaseStatus == null)
                return OperationResult.Failed($"Target case status for action '{action}' not found.");

            caseEntity.CaseStatusId = targetCaseStatus.Id;
            _context.Cases.Update(caseEntity);
            await _context.SaveChangesAsync();

            return OperationResult.Success(action == "Accept"
                ? "Case accepted successfully."
                : "Case rejected successfully.");
        }
    }
}
