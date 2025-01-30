using CaseManagement.Business.Common;
using CaseManagement.Business.Features.Case;
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
        //Task<int> GetCaseTypeId(string caseType);
        //Task<Person> GetPersonAsync(string name, long contact);
        //Task<int> GetCaseStatusId(string caseStatus);

        Task<OperationResult<string>> CreateCaseAsync(Case newCase);
        Task<OperationResult<string>> AssignAdvocate(AssignAdvocateCommand assignAdvocateCommand, Case existingCase);
        Task<OperationResult<Case>> GetCaseByIdAsync(string caseNumber);
        Task<OperationResult<IEnumerable<Case>>> GetAllCasesAsync();
        //Task<OperationResult<Case>> UpdateCaseAsync(Case updatedCase);
        //Task<OperationResult<string>> DeleteCaseAsync(string caseNumber);
    }
}
