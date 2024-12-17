

using CaseManagement.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace CaseManagement.DataAccess.Queries.Common
{
    public class LookupConstantQueryService
    {
        public static async Task<int> GetLookupConstantIdByCodeAndTypeAsync(string code, string type)
        {
            using (var context = new CaseManagementContext())
            {
                var lookupConstantdata = await context.LookupConstants.FirstOrDefaultAsync(c => c.Type == type && c.Code == code);
                return lookupConstantdata?.Id ?? 0;
            }
        }
    }
}
