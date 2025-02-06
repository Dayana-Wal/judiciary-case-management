using CaseManagement.Business.Commands;
using CaseManagement.Business.Common;
using CaseManagement.Business.Features.Case;
using CaseManagement.Business.Queries;
using CaseManagement.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CaseManagement.Business.Service
{
    public class CaseManager : BaseManager
    {
        private readonly ICaseCommandHandler _caseCommandHandler;
        private readonly ICaseFileCommandHandler _caseFileHandler;
        private readonly ICaseQueryHandler _caseQueryHandler;
        private readonly IPersonQueryHandler _personQueryHandler;


        public CaseManager(ICaseCommandHandler caseHandler, ICaseFileCommandHandler caseFileCommandHandler, ICaseQueryHandler caseQueryHandler, IPersonQueryHandler personQueryHandler)
        {
            _caseCommandHandler = caseHandler;
            _caseFileHandler = caseFileCommandHandler;
            _caseQueryHandler = caseQueryHandler;
            _personQueryHandler = personQueryHandler;
        }

        public async Task<OperationResult<string>> AssignAdvocate(AssignAdvocateCommand assignAdvocateCommand)
        {
            var existingCase = await _caseQueryHandler.GetExistingCase(assignAdvocateCommand.CaseId);
            var existingUser = await _personQueryHandler.GetUserRole(assignAdvocateCommand.AdvocateId);
            if (existingCase == null || existingUser == null)
            {
                return OperationResult<string>.Failed("Given case id or advocate not found in the db");
            }
            if(existingUser.Role.Code.ToUpper() != "ADV"){
                return OperationResult<string>.Failed("Given person is not an advocate");
            }
            var res = await _caseCommandHandler.AssignAdvocate(assignAdvocateCommand,existingCase);
            return res;


        }

        public async Task<OperationResult<List<string>>> RegisterCase(CreateCaseCommand command)
        {
            var dataStoredResult = new OperationResult<List<string>>();

                string caseId = NewUlid();

                var newCase = new Case
                {
                    Id = caseId,
                    Description = command.Description,
                    DateOfIncident = Convert.ToDateTime(command.DateOfIncident),
                    CaseTypeId = await _caseQueryHandler.GetCaseTypeId(command.CaseType),
                    CaseNumber = string.Concat("CASE-",DateTime.Now.ToString("yyyyMMddHHmmss"), "-",command.VictimName.Split("")[0]),
                    CaseStatusId = await _caseQueryHandler.GetCaseStatusId("Open"),
                };

                var victim = await _caseQueryHandler.GetPersonAsync(command.VictimName, command.VictimContact);
                var accused = await _caseQueryHandler.GetPersonAsync(command.AccusedName, command.AccusedContact);

                if (victim == null || accused == null)
                {
                    //TODO: If accused not in db, we need to insert accused details in Person 
                    return OperationResult<List<string>>.Failed(message: "Error creating or fetching victim/accused, registration failed.", data: []);
                }

                newCase.VictimId = victim.Id;
                newCase.AccusedId = accused.Id;

                var caseCreateResult = await _caseCommandHandler.CreateCaseAsync(newCase);

                if (caseCreateResult.Status == OperationStatus.Success)
                {
                //Store the fileIds and caseId in CaseFiles table
                    var caseFiles = new List<CaseFile>();
                    foreach(var fileId in command.FileIds)
                    {
                        caseFiles.Add(new CaseFile { Id = NewUlid(), CaseId = caseCreateResult.Data, FileId= fileId });
                    }
                    var res = await _caseFileHandler.AddCaseFile(caseFiles);
                    dataStoredResult = OperationResult<List<string>>.Success(data: [], message: "Case registered successfully!");
                }
                else
                {
                    List<string> tempErrors = new List<string> { caseCreateResult.Data };
                    dataStoredResult = OperationResult<List<string>>.Failed(data: tempErrors, message: caseCreateResult.Message);
                }
            return dataStoredResult;
        }
    }
}
