using CaseManagement.Business.Commands;
using CaseManagement.Business.Common;
using CaseManagement.Business.Features.Files;
using CaseManagement.Business.Queries;
using CaseManagement.DataAccess.Entities;


namespace CaseManagement.Business.Service
{
    public class FileManager : BaseManager
    {
        private readonly IFileCommandHandler _fileCommandHandler;
        private readonly ILookUpConstantsQuery _lookUpConstantsQuery;
        public FileManager(IFileCommandHandler fileCommandHandler,ILookUpConstantsQuery lookUpConstantsQuery)
        {
            _fileCommandHandler = fileCommandHandler;
            _lookUpConstantsQuery = lookUpConstantsQuery;
        }
        public async Task<OperationResult<List<string>>> UploadFile(FilesCommand filesCommand)
        {
                var uploadsPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
                Directory.CreateDirectory(uploadsPath);
                List<Files> filesToInsert = new List<Files>();
                var fileTypeId = await _lookUpConstantsQuery.GetConstantId(filesCommand.fileTypeCode, "File Type");

                foreach (var file in filesCommand.Files)
                {
                    var currentTimestamp = DateTime.Now.ToString("yyyyMMdd_HHmmssfff");
                    var fileName = $"{Path.GetFileNameWithoutExtension(file.FileName)}_{currentTimestamp}{Path.GetExtension(file.FileName)}";
                    var filePath = Path.Combine(uploadsPath, fileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    filesToInsert.Add(new Files
                    {
                        Id = NewUlid(),
                        FileName = fileName,
                        FilePath = Path.Combine("wwwroot","uploads" , fileName),
                        FileTypeId = fileTypeId,
                        UploadedBy = filesCommand.UploadedBy,
                    });
                }
                var res = await _fileCommandHandler.AddFilesAsync(filesToInsert);
                return res;
        }
    }
}
