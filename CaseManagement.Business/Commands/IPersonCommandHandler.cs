
using CaseManagement.Business.Common;
using CaseManagement.DataAccess.Entities;


namespace CaseManagement.DataAccess.Commands
{
    public interface IPersonCommandHandler
    {
        Task<OperationResult<string>> CreateUserAsync(Person person , User user);
        Task UpdateAsync<T>(Person person);
        Task DeleteAsync<T>(Person person);
    }
}
