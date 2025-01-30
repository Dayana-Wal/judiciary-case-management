using CaseManagement.Business.Common;
using CaseManagement.Business.Queries;
using CaseManagement.DataAccess.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseManagement.Business.Service
{
    public class AdvocateManager: BaseManager
    {
        private readonly IAdvocateQueryHandler _advocateQueryHandler;
        public AdvocateManager(IAdvocateQueryHandler advocateQueryHandler)
        {
            _advocateQueryHandler = advocateQueryHandler;
        }

        public async Task<OperationResult<List<AdvocateDto>>> GetAdvocateDetails()
        {
            var advocates = await _advocateQueryHandler.GetAdvocates();
            return OperationResult<List<AdvocateDto>>.Success(message:"Advocates with less than 3 active cases" ,data:advocates);
        }
    }
}
