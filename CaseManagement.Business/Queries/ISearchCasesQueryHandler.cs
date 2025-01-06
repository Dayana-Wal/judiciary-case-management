using System.Collections.Generic;
using System.Threading.Tasks;
using CaseManagement.DataAccess.Entities;
using CaseManagement.Web.Models;

namespace CaseManagement.Business.Queries
{
    public interface ISearchCasesQueryHandler
    {
        Task<List<Case>> SearchCasesAsync(CaseSearchQuery query);
    }
}


