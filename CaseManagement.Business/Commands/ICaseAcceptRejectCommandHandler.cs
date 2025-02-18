using CaseManagement.Business.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseManagement.Business.Commands
{
    public interface ICaseAcceptRejectCommandHandler
    {
        Task<OperationResult> TakeCaseActionAsync(string caseId, string action);
    }
}
