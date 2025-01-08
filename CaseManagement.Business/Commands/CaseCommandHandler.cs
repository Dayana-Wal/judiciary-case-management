using CaseManagement.Business.Common;
using CaseManagement.Business.Features.Case;
using CaseManagement.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CaseManagement.Business.Commands
{
    public class CaseCommandHandler : ICaseCommandHandler
    {
        private readonly CaseManagementContext _caseContext;

        public CaseCommandHandler(CaseManagementContext context)
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

        
       

        public async Task<OperationResult<string>> CreateCaseAsync(Case newCaseRaised)
        {
            
                 //&& c.DateOfIncident == newCaseRaised.DateOfIncident

                var existingCase = await _caseContext.Cases
                    .FirstOrDefaultAsync(c => c.Description == newCaseRaised.Description);

                if (existingCase != null)
                {
                    return OperationResult<string>.Failed(message: "A case with similar details already exists.", data: existingCase.CaseNumber);
                }


                var newCase = new Case
                {
                    Id = newCaseRaised.Id,

                    //VictimName = newCaseRaised.VictimName,
                    //VictimContact = newCaseRaised.VictimContact, // Update this with appropriate logic if necessary

                    //CaseTypeId = _caseContext.LookupConstants.FirstOrDefault(lc => lc.Text.ToString() == newCaseRaised.CaseType.ToString()).Id,
                    //CaseTypeId = 11,
                    //CaseTypeId = _caseContext.LookupConstants
                    //    .Where(c=>c.Text =="Civil" || c.Text == "Criminal" || c.Text == "Family" || c.Text=="Copyright" || c.Text == "Trade" || c.Text =="Secret" || c.Text =="Traffic")
                    //    .Select(c=>c.Id)
                    //    .FirstOrDefault(),

                    CaseTypeId = _caseContext.LookupConstants
                   .Where(c => new[] { "Civil", "Criminal", "Family", "Copyright", "Trade", "Secret", "Traffic" }
                   .Contains(c.Text))
                   .Select(c => c.Id)
                   .FirstOrDefault(),


                    Description = newCaseRaised.Description,
                    //todo - change case number format
                    CaseNumber = $"CASE-{DateTime.UtcNow.Ticks}",
                    DateOfIncident = newCaseRaised.DateOfIncident,

                    AccusedId = newCaseRaised.AccusedId,
                    VictimId = newCaseRaised.VictimId,
                    AdvocateId = "01JF7QPT5MQHTYKBDH92FEJ336",


                    //CaseStatusId = _caseContext.LookupConstants.FirstOrDefault(lc => lc.Text.ToString() == "Open")?.Id ?? 0
                    CaseStatusId = _caseContext.LookupConstants
                        .Where(c => c.Text == "Open")
                        .Select(c => c.Id)
                        .FirstOrDefault()
                };

                await _caseContext.Cases.AddAsync(newCase);
                await _caseContext.SaveChangesAsync();

                return OperationResult<string>.Success("Case created successfully!");
            
            //catch (Exception ex)
            //{
            //    return OperationResult<string>.Failed(message: $"An error occurred while creating the case. {ex.Message}", data: ex.Message);
            //}
        }

        public async Task<OperationResult<Case>> GetCaseByIdAsync(string caseNumber)
        {
            
                var caseData = await _caseContext.Cases
                    .Join(_caseContext.People,
                        c => c.VictimId,
                        p => p.Id,
                        (c, p) => new Case
                        {
                            CaseNumber = c.CaseNumber,
                            Description = c.Description,
                            CaseStatusId = c.CaseStatusId,
                            
                            DateOfIncident = c.DateOfIncident,
                            
                            //VictimName = p.Name,
                            //VictimContact = p.Contact
                        })
                    .FirstOrDefaultAsync(c => c.CaseNumber == caseNumber);
                if (caseData == null)
                {
                    return OperationResult<Case>.Failed(message: "Case not found.", data: null);
                }

                return OperationResult<Case>.Success(data: caseData, message: "Case found!");
            
            //catch (Exception ex)
            //{
            //    return OperationResult<Case>.Failed(message: $"An error occurred while retrieving the case: {ex.Message}", data: null);
            //}
        }

        public async Task<OperationResult<IEnumerable<Case>>> GetAllCasesAsync()
        {
            
                var cases = await _caseContext.Cases.ToListAsync();

                if (cases.Any())
                {
                    return OperationResult<IEnumerable<Case>>.Success(data: cases, message: "Cases retrieved successfully!");
                }

                else
                {
                    return OperationResult<IEnumerable<Case>>.Failed(message: "No case found.", data: null);
                }
            
            //catch (Exception ex)
            //{
            //    return OperationResult<IEnumerable<Case>>.Failed(message: $"An error occurred while retrieving all cases: {ex.Message}", data: null);
            //}
        }
        
    }
}
