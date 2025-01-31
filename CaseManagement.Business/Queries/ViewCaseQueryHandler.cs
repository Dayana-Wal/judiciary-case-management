using CaseManagement.DataAccess.DTO;
using CaseManagement.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace CaseManagement.Business.Queries
{
    public class ViewCaseQueryHandler : IViewCaseQueryHandler
    {
        private readonly CaseManagementContext _context;

        public ViewCaseQueryHandler(CaseManagementContext context)
        {
            _context = context;
        }

        public async Task<CaseDetailsDto?> GetCaseDetailsAsync(string caseId)
        {
            var caseEntity = await _context.Cases
                .Include(c => c.Victim)
                .Include(c => c.Accused)
                .Include(c => c.Advocate)
                .FirstOrDefaultAsync(c => c.Id == caseId);

            if (caseEntity == null)
                return null;

            return new CaseDetailsDto
            {
                CaseNumber = caseEntity.CaseNumber,
                CaseStatus = caseEntity.CaseStatus?.Text ?? "",
                Victim = new PersonDto
                {
                    Name = caseEntity.Victim?.Name ?? "",
                    Contact = caseEntity.Victim?.Contact ?? 0,
                    Email = caseEntity.Victim?.Email ?? ""
                },
                Accused = new PersonDto
                {
                    Name = caseEntity.Accused?.Name ?? "",
                    Contact = caseEntity.Accused?.Contact ?? 0,
                    Email = caseEntity.Accused?.Email ?? ""
                },
                Advocate = caseEntity.Advocate != null
                    ? new PersonDto
                    {
                        Name = caseEntity.Advocate.Name,
                        Contact = caseEntity.Advocate.Contact,
                        Email = caseEntity.Advocate.Email
                    }
                    : null
            };
        }
    }
}
