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
            var advocates = await _context.People.FromSqlInterpolated($@"SELECT
                                p.Id, p.Name, p.Contact, p.Email, lc.Code
                                FROM Person p
                                INNER JOIN[User] u ON p.Id = u.PersonId
                                INNER JOIN LookupConstant lc ON lc.Id = u.RoleId
                                LEFT JOIN[Case] c ON c.AdvocateId = p.Id
                                LEFT JOIN LookupConstant clc ON clc.Id = c.CaseStatusId
                                WHERE
                                    lc.Type = {AdvocateRoleConstant.UserRoleType}
                                    AND lc.Code = {AdvocateRoleConstant.AdvocateRoleCode}
                                    AND(c.Id IS NULL OR clc.Code NOT IN('CLS', 'REJ'))
                                GROUP BY
                                    p.Id, p.Name, p.Contact, p.Email, lc.Code
                                HAVING
                                    COUNT(c.Id) <= {AdvocateRoleConstant.ActiveCaseCount} ")
                        .Select(p => new AdvocateDto
                        {
                            Id = p.Id,
                            Name = p.Name,
                            Contact = p.Contact,
                            Email = p.Email,
                        })
                        .ToListAsync();
            return advocates;
        }
    }
}
