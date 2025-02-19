using CaseManagement.DataAccess.Entities;
using CaseManagement.Business.Queries;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CaseManagement.Web.Models;

namespace CaseManagement.Business.Service
{
    public class CaseSearchManager : BaseManager
    {
        private readonly ISearchCasesQueryHandler _searchCasesQueryHandler;

        public CaseSearchManager(ISearchCasesQueryHandler searchCasesQueryHandler)
        {
            _searchCasesQueryHandler = searchCasesQueryHandler;
        }

        public async Task<List<Case>> SearchCasesAsync(CaseSearchQuery searchQuery)
        {
            var cases = await _searchCasesQueryHandler.SearchCasesAsync(searchQuery);
            return cases;
        }

        public async Task<List<Case>> GetOpenCasesAsync()
        {
            var openCases = await _searchCasesQueryHandler.GetOpenCasesAsync();
            return openCases;
        }
    }
}




