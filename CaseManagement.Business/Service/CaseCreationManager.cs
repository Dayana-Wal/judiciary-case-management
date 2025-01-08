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
            var dataStoredResult = new OperationResult<List<string>>();

            
                string caseId = NewUlid();

                var newCase = new Case
                {
                    Id = caseId,
                    Description = command.Description,
                    DateOfIncident = (DateTime)command.DateOfIncident,
                    CaseTypeId = await _caseHandler.GetCaseTypeId(command.CaseType),
                    CaseNumber = $"CASE-{DateTime.UtcNow.Ticks}",
                    //AdvocateId = "A1",
                    CaseStatusId = await _caseHandler.GetCaseStatusId("Open"),
                    //case = newCase.CaseType
                };

                //var victim = await _caseHandler.GetPersonAsync(command.VictimName, command.VictimContact);9999000090, 9728939290
                var victim = await _caseHandler.GetPersonAsync(command.VictimName, command.VictimContact);
                var accused = await _caseHandler.GetPersonAsync(command.AccusedName, command.AccusedContact);

                if (victim == null || accused == null)
                {
                    return OperationResult<List<string>>.Failed(message: "Error creating or fetching victim/accused, registration failed.", data: []);
                }

                newCase.VictimId = victim.Id;
                newCase.AccusedId = accused.Id;

                //var dataStoredResult = new OperationResult<List<string>>();


                var caseCreateResult = await _caseHandler.CreateCaseAsync(newCase);

                if (caseCreateResult.Status == OperationStatus.Success)
                {
                    dataStoredResult = OperationResult<List<string>>.Success(data: [], message: "Case registered successfully!");
                }
                else
                {
                    List<string> tempErrors = new List<string> { caseCreateResult.Data };
                    dataStoredResult = OperationResult<List<string>>.Failed(data: tempErrors, message: caseCreateResult.Message);
                }
            //}

            //catch (Exception ex)
            //{
            //    List<string> tempErrors = new List<string> { ex.Message };
            //    dataStoredResult = OperationResult<List<string>>.Failed(data: tempErrors, message: $"An error occurred while registering the case.{ex.Message}");
            //}
            return dataStoredResult;
        }
    }
}
