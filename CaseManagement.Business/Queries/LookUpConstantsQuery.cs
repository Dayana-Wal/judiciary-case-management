using CaseManagement.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseManagement.Business.Queries
{
    public class LookUpConstantsQuery: ILookUpConstantsQuery
    {
        private readonly CaseManagementContext _context;
        public LookUpConstantsQuery(CaseManagementContext context)
        {
            _context = context;
        }

        public async Task<int> GetConstantId(string code, string type)
        {
            var constantId = await _context.LookupConstants
                           .Where(entry => entry.Code == code && entry.Type == type)
                           .Select(entry => entry.Id)
                           .FirstOrDefaultAsync();
            return constantId;
        }
    }
}
