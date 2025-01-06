using CaseManagement.DataAccess.Entities;
using CaseManagement.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace CaseManagement.Business.Queries
{
    public class SearchCaseQueryHandler : ISearchCasesQueryHandler
    {
        private readonly CaseManagementContext _context;

        public SearchCaseQueryHandler(CaseManagementContext context)
        {
            _context = context;
        }

        public async Task<List<Case>> SearchCasesAsync(CaseSearchQuery query)
        {
            IQueryable<Case> casesQuery = _context.Cases
                .Include(c => c.Accused)
                .Include(c => c.Victim)
                .Include(c => c.Advocate)
                .Include(c => c.CaseStatus);

            if (!string.IsNullOrEmpty(query.SearchCategory) && !string.IsNullOrEmpty(query.SearchValue))
            {
                casesQuery = FilterCasesBySearchCriteria(casesQuery, query.SearchCategory, query.SearchValue);
            }

            return await casesQuery.ToListAsync();
        }

        private IQueryable<Case> FilterCasesBySearchCriteria(IQueryable<Case> query, string category, string value)
        {
            return category switch
            {
                "CaseNumber" => query.Where(c => c.CaseNumber == value),
                "VictimName" => query.Where(c => c.Victim.Name == value),
                "AccusedName" => query.Where(c => c.Accused.Name == value),
                "AdvocateName" => query.Where(c => c.Advocate != null && c.Advocate.Name == value),
                "EmailAddress" => query.Where(c => c.Victim.Email == value ||
                                                   c.Accused.Email == value ||
                                                   (c.Advocate != null && c.Advocate.Email == value)),
                "PhoneNumber" => long.TryParse(value, out var phoneNumber)
                    ? query.Where(c =>
                          c.Victim.Contact == phoneNumber ||
                          c.Accused.Contact == phoneNumber ||
                          (c.Advocate != null && c.Advocate.Contact == phoneNumber))
                    : query
            };
        }
    }
}
