using CaseManagement.DataAccess.Entities;
using System.Runtime.Caching;

namespace CaseManagement.Business.Utility
{
    public class CacheUtility
    {
        private readonly CaseManagementContext _caseManagementContext;
        private readonly MemoryCache _cache;

        public CacheUtility(CaseManagementContext caseManagementContext)
        {
            _caseManagementContext = caseManagementContext;
            _cache = MemoryCache.Default;
        }

        public List<string> GetCaseTypes()
        {
            const string cacheKey = "CaseTypes";

            if (!_cache.Contains(cacheKey))
            {
                List<string> caseTypes = _caseManagementContext.LookupConstants
                    .Where(c => c.Type == "Case Type")
                    .Select(c => c.Text)
                    .ToList();

                CacheItemPolicy policy = new CacheItemPolicy
                {
                    AbsoluteExpiration = DateTimeOffset.UtcNow.AddMinutes(30)
                };

                _cache.Add(cacheKey, caseTypes, policy);
            }
            return (List<string>)_cache.Get(cacheKey);
        }
    }
}
