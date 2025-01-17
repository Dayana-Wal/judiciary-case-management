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

        public async Task<OperationResult<string>> CreateCaseAsync(Case newCaseRaised)
        {
            var existingCase = await _caseContext.Cases
                .FirstOrDefaultAsync(c => c.Description == newCaseRaised.Description);

            if (existingCase != null)
            {
                return OperationResult<string>.Failed(message: "A case with similar details already exists.", data: existingCase.CaseNumber);
            }

            await _caseContext.Cases.AddAsync(newCaseRaised);
            await _caseContext.SaveChangesAsync();

            return OperationResult<string>.Success(data: newCaseRaised.Id, message: "Case created successfully!");

        }


        //public async Task<int> GetCaseTypeId(string caseType)
        //{
        //    var caseTypeEntity = await _caseContext.LookupConstants
        //        .FirstOrDefaultAsync(lc => lc.Text.ToString() == caseType); 

        //    if (caseTypeEntity == null)
        //    {
        //        throw new InvalidOperationException("Case type not found.");
        //    }

        //    return caseTypeEntity.Id; 
        //}

        //public async Task<Person> GetPersonAsync(string name, long contact)
        //{
        //    var person = await _caseContext.People
        //        .FirstOrDefaultAsync(p => p.Name == name && p.Contact == contact);

        //    if (person != null)
        //    {
        //        return person; 
        //    }
        //    return null;

        //}


     
        //public async Task<int> GetCaseStatusId(string caseStatus)
        //{
        //    var caseStatusEntity = await _caseContext.LookupConstants
        //        .FirstOrDefaultAsync(lc => lc.Text.ToString() == caseStatus);

        //    if (caseStatusEntity == null)
        //    {
        //        throw new InvalidOperationException("Case status not found.");
        //    }

        //    return caseStatusEntity.Id;
        //}


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
