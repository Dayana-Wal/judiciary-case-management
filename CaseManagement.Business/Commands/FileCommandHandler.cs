using CaseManagement.Business.Common;
using CaseManagement.DataAccess.Entities;


namespace CaseManagement.Business.Commands
{
    public class FileCommandHandler : IFileCommandHandler
    {
        private readonly CaseManagementContext _context;

        public FileCommandHandler(CaseManagementContext context)
        {
            _context = context;
        }

        public async Task<OperationResult<List<string>>> AddFilesAsync(List<Files> files)
        {

            await _context.Files.AddRangeAsync(files);
            await _context.SaveChangesAsync();

            var insertedFileIds = files.Select(file => file.Id).ToList();
            return OperationResult<List<string>>.Success(insertedFileIds, "Successfully added to database");

        }
    }
}
