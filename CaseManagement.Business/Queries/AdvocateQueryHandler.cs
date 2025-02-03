using CaseManagement.Business.Utility;
using CaseManagement.DataAccess.DTO;
using CaseManagement.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using Twilio.TwiML.Voice;

namespace CaseManagement.Business.Queries
{
    public class AdvocateQueryHandler : IAdvocateQueryHandler
    {
        private readonly CaseManagementContext _context;
        public AdvocateQueryHandler(CaseManagementContext context)
        {
            _context = context;
        }
        public async Task<List<AdvocateDto>> GetAdvocates()
        {
            string sqlQuery = string.Format(@"
                SELECT p.Id, p.Name, p.Contact, p.Email, lc.Code, COUNT(c.Id) AS ActiveCases
                FROM Person p
                INNER JOIN [User] AS u ON p.Id = u.PersonId
                INNER JOIN LookupConstant lc ON lc.Id = u.RoleId
                LEFT JOIN [Case] AS c ON c.AdvocateId = p.Id
                LEFT JOIN LookupConstant clc ON clc.Id = c.CaseStatusId
                WHERE lc.Type = '{0}'
                    AND lc.Code = '{1}'
                    AND (c.Id IS NULL OR clc.Code NOT IN ('CLS', 'REJ'))
                GROUP BY p.Id, p.Name, p.Contact, p.Email, lc.Code
                HAVING COUNT(c.Id) < {2}",
                AdvocateRoleConstant.UserRoleType,
                AdvocateRoleConstant.AdvocateRoleCode,
                AdvocateRoleConstant.ActiveCaseCount);

            var advocates = await _context.Database.SqlQueryRaw<AdvocateDto>(sqlQuery).ToListAsync();


            return advocates;
        }
    }
}
