//using CaseManagement.DataAccess.Entities;
//using Microsoft.Extensions.Caching.Memory;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace CaseManagement.Business.Utility
//{
//    public class CacheUtility
//    {
//        private readonly CaseManagementContext _caseManagementContext;
//        private readonly IMemoryCache _memoryCache;

//        public CacheUtility(CaseManagementContext caseManagementContext , IMemoryCache memoryCache)
//        {
//            _caseManagementContext = caseManagementContext;
//            _memoryCache = memoryCache;
//        }

//        public List<string> GetCaseTypes()
//        {
//            const string cacheKey = "CaseTypes";

//            if (_memoryCache.TryGetValue(cacheKey, out List<string> CaseTypes))
//            {
//                CaseTypes = _caseManagementContext.LookupConstants
//                    .Select(c => c.Text)
//                    .ToList();

//                _memoryCache.Set(cacheKey, CaseTypes);

//            }


//            return CaseTypes;
//        }


//    }
//}

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
