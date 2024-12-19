using CaseManagement.DataAccess.Entities;

namespace CaseManagement.DataAccess.Queries
{
    public class PersonQueryHandler : IPersonQueryHandler
    {
        private readonly CaseManagementContext _context;

        public PersonQueryHandler(CaseManagementContext context)
        {
            _context = context;
        }

        Task<Person> IPersonQueryHandler.GetPersonByEmailAsync { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Task<Person> GetPersonByNameAsync { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Task<User> GetUserByEmailAsync { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

       

       

    }
}
