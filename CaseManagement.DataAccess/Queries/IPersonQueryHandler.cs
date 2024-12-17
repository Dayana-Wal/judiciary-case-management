
using CaseManagement.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseManagement.DataAccess.Queries
{
    public interface IPersonQueryHandler
    {
        Task<Person> GetPersonByEmailAsync(string email);

        Task<Person> GetPersonByNameAsync(string name);

        Task<User> GetUserByEmailAsync(string email);
        Task<User> GetUserByUserNameAsync(string userName);
    }
}
