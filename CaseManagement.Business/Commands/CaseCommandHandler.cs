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
                .FirstOrDefaultAsync(lc => lc.Text.ToString() == caseType); // Assuming case type is stored as a string

            if (caseTypeEntity == null)
            {
                throw new InvalidOperationException("Case type not found.");
            }

            return caseTypeEntity.Id; // Return the CaseTypeId
        }

        // Method to get or create a Person based on name and contact
        public async Task<Person> GetPersonAsync(string name, int contact)
        {
            // Try to find the person by name and contact (if provided)
            var person = await _caseContext.People
                .FirstOrDefaultAsync(p => p.Name == name && p.Contact == contact);

            if (person != null)
            {
                return person; // Return existing person if found
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

        //public async Task<OperationResult<string>> CreateCaseAsync(Case newCase)
        //{
        //    try
        //    {
        //        var existingCase = await _caseContext.Cases
        //            .FirstOrDefaultAsync(c => c.Description == newCase.Description);
        //        if (existingCase != null)
        //        {
        //            return OperationResult<string>.Failed(message: "Case with the same description already exists.", data: existingCase.CaseNumber);
        //        }

        //        await _caseContext.Cases.AddAsync(newCase);
        //        await _caseContext.SaveChangesAsync();

        //        return OperationResult<string>.Success("Case created successfully!");
        //    }
        //    catch (Exception ex)
        //    {
        //        return OperationResult<string>.Failed(message: "An exception occurred while creating the case", data: ex.Message);
        //    }
        //}

        public async Task<OperationResult<string>> CreateCaseAsync(Case newCaseRaised)
        {
            try
            {

                var existingCase = await _caseContext.Cases
                    .FirstOrDefaultAsync(c => c.Description == newCaseRaised.Description && c.DateOfIncident == newCaseRaised.DateOfIncident);

                if (existingCase != null)
                {
                    return OperationResult<string>.Failed(message: "A case with similar details already exists.", data: existingCase.CaseNumber);
                }


                var newCase = new Case
                {
                    Id = Guid.NewGuid().ToString(),
                    VictimName = newCaseRaised.VictimName,
                    VictimContact = newCaseRaised.VictimContact, // Update this with appropriate logic if necessary
                    CaseTypeId = _caseContext.LookupConstants.FirstOrDefault(lc => lc.Text.ToString() == newCaseRaised.CaseType.ToString())?.Id ?? 0,
                    Description = newCaseRaised.Description,
                    CaseNumber = $"CASE-{DateTime.UtcNow.Ticks}",
                    DateOfIncident = newCaseRaised.DateOfIncident,
                    
                    AccusedId = Guid.NewGuid().ToString(), // Replace with actual logic
                    VictimId = Guid.NewGuid().ToString(),  // Replace with actual logic
                    
                    CaseStatusId = _caseContext.LookupConstants.FirstOrDefault(lc => lc.Text == "Open")?.Id ?? 0,
                };

                // Add the new case to the database
                await _caseContext.Cases.AddAsync(newCase);
                await _caseContext.SaveChangesAsync();

                return OperationResult<string>.Success("Case created successfully!");
            }
            catch (Exception ex)
            {
                return OperationResult<string>.Failed(message: "An error occurred while creating the case.", data: ex.Message);
            }
        }

        public async Task<OperationResult<Case>> GetCaseByIdAsync(string caseNumber)
        {
            try
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
                            
                            VictimName = p.Name,
                            VictimContact = p.Contact
                        })
                    .FirstOrDefaultAsync(c => c.CaseNumber == caseNumber);
                if (caseData == null)
                {
                    return OperationResult<Case>.Failed(message: "Case not found.", data: null);
                }

                return OperationResult<Case>.Success(data: caseData, message: "Case found!");
            }
            catch (Exception ex)
            {
                return OperationResult<Case>.Failed(message: $"An error occurred while retrieving the case: {ex.Message}", data: null);
            }
        }

        public async Task<OperationResult<IEnumerable<Case>>> GetAllCasesAsync()
        {
            try
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
            }
            catch (Exception ex)
            {
                return OperationResult<IEnumerable<Case>>.Failed(message: $"An error occurred while retrieving all cases: {ex.Message}", data: null);
            }
        }

        public async Task<OperationResult<Case>> UpdateCaseAsync(Case updatedCase)
        {
            try
            {
                var existingCase = await _caseContext.Cases.FindAsync(updatedCase.CaseNumber);

                if (existingCase == null)
                {
                    return OperationResult<Case>.Failed(message: "Case not found, check the details again", data: null);
                }

                existingCase.Description = updatedCase.Description;
                existingCase.CaseStatus = updatedCase.CaseStatus;

                _caseContext.Cases.Update(existingCase);
                await _caseContext.SaveChangesAsync();

                return OperationResult<Case>.Success(data: existingCase, message: "Case details updated");
            }

            catch (Exception ex)
            {
                return OperationResult<Case>.Failed(message: $"An error occurred while updating the case: {ex.Message}", data: null);
            }
        }

        public async Task<OperationResult<string>> DeleteCaseAsync(string caseNumber)
        {
            try
            {
                var caseToDelete = await _caseContext.Cases.FindAsync(caseNumber);
                if (caseToDelete == null)
                {
                    return OperationResult<string>.Failed(message: "Case not found.", data: null);
                }



                _caseContext.Cases.Remove(caseToDelete);
                await _caseContext.SaveChangesAsync();

                return OperationResult<string>.Success(data: caseToDelete.CaseNumber, message: "Case data deleted successfully!");
            }
            catch (Exception ex)
            {
                return OperationResult<string>.Failed(message: $"An error occurred while deleting the case: {ex.Message}", data: null);
            }
        }
    }
}
