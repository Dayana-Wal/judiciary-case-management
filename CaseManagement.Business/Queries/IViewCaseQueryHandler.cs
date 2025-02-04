using CaseManagement.Business.Features.Case;
using CaseManagement.DataAccess.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseManagement.Business.Queries
{
    public interface IViewCaseQueryHandler
    {
        Task<CaseDetailsDto?> GetCaseDetailsAsync(string caseId);
    }

}
