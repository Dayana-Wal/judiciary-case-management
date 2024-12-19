using CaseManagement.DataAccess.Entities;

namespace CaseManagement.DataAccess.Queries
{
    public interface IPersonQueryHandler
    {
        Task<Person> GetPersonByEmailAsync { get; set; }

        Task<Person> GetPersonByNameAsync {  get; set; }

        Task<User> GetUserByEmailAsync { get; set; }
    }
}
