using CaseManagement.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace CaseManagement.Business.Providers
{
    public class RoleIdProvider
    {
        private readonly CaseManagementContext _context;

        public RoleIdProvider(CaseManagementContext context)
        {
            _context = context;
        }
        public async Task<int> GetRoleId(string code, string type)
        {
            var lookupConstantdata = await _context.LookupConstants.FirstOrDefaultAsync(c => c.Type == type && c.Code == code);
            return lookupConstantdata?.Id ?? 0;
            
        }
    }
}