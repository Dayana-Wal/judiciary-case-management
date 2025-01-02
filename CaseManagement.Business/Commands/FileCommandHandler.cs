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

        public async Task<OperationResult<string>> AddFileAsync(DataAccess.Entities.File file)
        {
            try
            {
                await _context.Files.AddAsync(file);
                await _context.SaveChangesAsync();
                //TODO: send the inserted file id
                return OperationResult<string>.Success("File details stored successfully!");
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return OperationResult<string>.Failed($"{ex.Message}");

            }

        }
    }
}
