using CaseManagement.Business.Common;

namespace CaseManagement.Business.Commands
{
    public interface IFileCommandHandler
    {
        Task<OperationResult<string>> AddFileAsync(DataAccess.Entities.File file);
    }
}
