using CaseManagement.Business.Common;
using CaseManagement.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseManagement.Business.Commands
{
    public interface ICaseFileCommandHandler
    {
        public Task<OperationResult> AddCaseFile(List<CaseFile> caseFiles);
    }
}
