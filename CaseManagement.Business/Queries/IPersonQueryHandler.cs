
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
        Task<Person> GetPersonByEmailAsync { get; set; }

        Task<Person> GetPersonByNameAsync {  get; set; }

        Task<User> GetUserByEmailAsync { get; set; }
    }
}
