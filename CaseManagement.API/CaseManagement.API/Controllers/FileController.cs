using CaseManagement.Business.Commands;
using CaseManagement.Business.Common;
using CaseManagement.Business.Features.Files;
using CaseManagement.Business.Queries;
using CaseManagement.Business.Service;
using CaseManagement.Business.Utility;
using Microsoft.AspNetCore.Mvc;

namespace CaseManagement.API.Controllers
{
    public class FileController : BaseController
    {
        private readonly IPersonQueryHandler _personQueryHandler;
        private readonly FileManager _fileManager;
        public FileController(IPersonQueryHandler personQueryHandler, FileManager fileManager)
        {
            _personQueryHandler = personQueryHandler;
            _fileManager = fileManager;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadFile([FromForm] FilesCommand filesCommand)
        {
            if (filesCommand == null)
            {
                return ToResponse(OperationResult.Failed("Invalid data to upload files"));
            }

            var validationResult = filesCommand.Validate();
            if (!validationResult.IsValid)
            {
                var validationErrors = validationResult.GetErrors();
                var returnResponse = OperationResult<List<string>>.ValidationError(data: validationErrors);
                return ToResponse(returnResponse);
            }

            var user = await _personQueryHandler.GetUserAsync(filesCommand.UploadedBy);
            if (user == null)
            {
                var opResult = OperationResult.Failed("User not found with the given userName for uploadedBy field");
                return ToResponse(opResult);
            }
            filesCommand.UploadedBy = user.Id;

            var res = await _fileManager.UploadFile(filesCommand);

            return ToResponse(res);
        }
    }
}
