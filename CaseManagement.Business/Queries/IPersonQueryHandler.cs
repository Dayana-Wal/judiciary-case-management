using CaseManagement.DataAccess.Entities;

namespace CaseManagement.DataAccess.Queries
{
    public interface IPersonQueryHandler
    {
        Task<User> GetUserAsync(string userName);
    }
}
