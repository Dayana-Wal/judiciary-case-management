using CaseManagement.DataAccess.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseManagement.Business.Queries
{
    public interface IAdvocateQueryHandler
    {
        public Task<List<AdvocateDto>> GetAdvocates();
    }
}
