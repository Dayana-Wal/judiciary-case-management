using CaseManagement.DataAccess.Entities;

namespace CaseManagement.Business.Queries
{
    public interface IPersonQueryHandler
    {
        Task<User> GetUserAsync(string userName);
        Task<Person> GetPersonAsync(string personName);

    }
}
