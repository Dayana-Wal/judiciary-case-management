using CaseManagement.Business.Commands;
using CaseManagement.Business.Common;
using CaseManagement.Business.Features.Files;
using CaseManagement.DataAccess.Entities;


namespace CaseManagement.Business.Service
{
    public class FileManager : BaseManager
    {
        private readonly IFileCommandHandler _fileCommandHandler;
        public FileManager(IFileCommandHandler fileCommandHandler)
        {
            _fileCommandHandler = fileCommandHandler;
        }
        public async Task<OperationResult<List<string>>> UploadFile(FilesCommand filesCommand)
        {
                var uploadsPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
                Directory.CreateDirectory(uploadsPath);
                List<Files> filesToInsert = new List<Files>();

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
                        FileTypeId = filesCommand.FileTypeId,
                        UploadedBy = filesCommand.UploadedBy,
                    });
                }
                var res = await _fileCommandHandler.AddFilesAsync(filesToInsert);
                return res;
        }
    }
}
