using CaseManagement.Business.Common;
using CaseManagement.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseManagement.Business.Commands
{
    public class CaseFileCommandHandler : ICaseFileCommandHandler
    {
        private readonly CaseManagementContext _context;
        public CaseFileCommandHandler(CaseManagementContext context)
        {
            _context = context;
        }
        public async Task<OperationResult> AddCaseFile(List<CaseFile> caseFiles)
        {
            await _context.CaseFiles.AddRangeAsync(caseFiles);
            await _context.SaveChangesAsync();

            return OperationResult.Success("Added case files successfully");
        }
    }
}
