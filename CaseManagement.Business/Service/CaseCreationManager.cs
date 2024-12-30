using CaseManagement.Business.Commands;
using CaseManagement.Business.Common;
using CaseManagement.Business.Features.Case;
using CaseManagement.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CaseManagement.Business.Service
{
    public class CaseCreationManager : BaseManager
    {
        private readonly ICaseCommandHandler _caseHandler;

        public CaseCreationManager(ICaseCommandHandler caseHandler)
        {
            _caseHandler = caseHandler;
        }

        public async Task<OperationResult<List<string>>> RegisterCase(CreateCaseCommand command)
        {
            // Generate unique case ID
            string caseId = NewUlid();

            // Create the new case object using the provided command data
            var newCase = new Case
            {
                Id = caseId,
                Description = command.Description,
                DateOfIncident = command.DateOfIncident,
                CaseTypeId = _caseHandler.GetCaseTypeId(command.CaseType), // Assuming method to get CaseTypeId
                CaseNumber = $"CASE-{DateTime.UtcNow.Ticks}",
                CaseStatusId = await _caseHandler.GetCaseStatusId("Open"), // Assuming method to get CaseStatusId
            };

            // Create or fetch Victim and Accused persons
            var victim = await _caseHandler.GetPersonAsync(command.VictimName, command.VictimContact);
            var accused = await _caseHandler.GetPersonAsync(command.AccusedName, 0); // Assuming no contact is provided for accused

            if (victim == null || accused == null)
            {
                return OperationResult<List<string>>.Failed(new List<string> { "Error creating or fetching victim/accused." });
            }

            // Assign the corresponding IDs to the new case
            newCase.VictimId = victim.Id;
            newCase.AccusedId = accused.Id;

            // Result object to store operation status and data
            var dataStoredResult = new OperationResult<List<string>>();

            try
            {
                // Call the case handler to create the case in the database
                var caseCreateResult = await _caseHandler.CreateCaseAsync(newCase);

                if (caseCreateResult.Status == OperationStatus.Success)
                {
                    // If the case was successfully created, return a success message with an empty list of errors
                    dataStoredResult = OperationResult<List<string>>.Success(data: new List<string>(), message: "Case registered successfully!");
                }
                else
                {
                    // If there was an issue with case creation, collect the errors and return failure
                    List<string> tempErrors = new List<string> { caseCreateResult.Data };
                    dataStoredResult = OperationResult<List<string>>.Failed(data: tempErrors, message: caseCreateResult.Message);
                }
            }
            catch (Exception ex)
            {
                // Handle any unexpected errors during case creation
                List<string> tempErrors = new List<string> { ex.Message };
                dataStoredResult = OperationResult<List<string>>.Failed(data: tempErrors, message: "An error occurred while registering the case.");
            }

            // Return the result of the case registration process
            return dataStoredResult;
        }
    }
}
