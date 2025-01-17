using CaseManagement.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseManagement.Business.Queries
{
    public class CaseQueryHandler : ICaseQueryHandler
    {
        private readonly CaseManagementContext _caseContext;

        public CaseQueryHandler(CaseManagementContext context)
        {
            _caseContext = context;
        }
        public async Task<int> GetCaseTypeId(string caseType)
        {
            var caseTypeEntity = await _caseContext.LookupConstants
                .FirstOrDefaultAsync(lc => lc.Text.ToString() == caseType);

            if (caseTypeEntity == null)
            {
                throw new InvalidOperationException("Case type not found.");
            }

            return caseTypeEntity.Id;
        }

        public async Task<Person> GetPersonAsync(string name, long contact)
        {
            var person = await _caseContext.People
                .FirstOrDefaultAsync(p => p.Name == name && p.Contact == contact);

            if (person != null)
            {
                return person;
            }
            return null;

        }

        public async Task<int> GetCaseStatusId(string caseStatus)
        {
            var caseStatusEntity = await _caseContext.LookupConstants
                .FirstOrDefaultAsync(lc => lc.Text.ToString() == caseStatus);

            if (caseStatusEntity == null)
            {
                throw new InvalidOperationException("Case status not found.");
            }

            return caseStatusEntity.Id;
        }
    }
}
