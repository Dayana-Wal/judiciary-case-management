using CaseManagement.Business.Commands;
using CaseManagement.Business.Features.Files;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseManagement.Business.Service
{
    public class FileManager : BaseManager
    {
        private readonly IFileCommandHandler _fileCommandHandler;
        public FileManager(IFileCommandHandler fileCommandHandler)
        {
            _fileCommandHandler = fileCommandHandler;
        }
        public async Task UploadFile(FilesCommand filesCommand)
        {
            var uploadsPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", $"{filesCommand.UploadedBy}");
            if (!Directory.Exists(uploadsPath))
            {
                Directory.CreateDirectory(uploadsPath);
            }

            foreach (var file in filesCommand.Files)
            {
                var currentTimestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                var fileName = $"{Path.GetFileNameWithoutExtension(file.FileName)}_{currentTimestamp}{Path.GetExtension(file.FileName)}";
                var filePath = Path.Combine(uploadsPath, fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                DataAccess.Entities.File newFile = new DataAccess.Entities.File
                {
                    Id = NewUlid(),
                    FileName = fileName,
                    FilePath = filePath,
                    FileTypeId = filesCommand.FileTypeId,
                    UploadedBy = filesCommand.UploadedBy,
                };

                var res = await _fileCommandHandler.AddFileAsync(newFile);
            }
        }
    }
}
