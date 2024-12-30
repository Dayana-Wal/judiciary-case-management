using CaseManagement.Business.Common;
using CaseManagement.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseManagement.Business.Commands
{
    public interface ICaseCommandHandler
    {
        Task<OperationResult<string>> CreateCaseAsync(Case newCase);
        Task<OperationResult<Case>> GetCaseByIdAsync(string caseNumber);
        Task<OperationResult<IEnumerable<Case>>> GetAllCasesAsync();
        Task<OperationResult<Case>> UpdateCaseAsync(Case updatedCase);
        Task<OperationResult<string>> DeleteCaseAsync(string caseNumber);
    }
}
