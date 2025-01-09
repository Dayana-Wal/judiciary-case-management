using CaseManagement.Business.Common;
using CaseManagement.DataAccess.Entities;

namespace CaseManagement.Business.Commands
{
    public interface IFileCommandHandler
    {
        Task<OperationResult<List<string>>> AddFilesAsync(List<Files> file);
    }
}
