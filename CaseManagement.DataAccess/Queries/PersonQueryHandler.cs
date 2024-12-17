


using CaseManagement.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseManagement.DataAccess.Queries
{
    public class PersonQueryHandler : IPersonQueryHandler
    {
        private readonly CaseManagementContext _context;

        public PersonQueryHandler(CaseManagementContext context)
        {
            _context = context;
        }

        public Task<Person> GetPersonByEmailAsync(string email)
        {
            throw new NotImplementedException();
        }

        public Task<Person> GetPersonByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetUserByEmailAsync(string email)
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetUserByUserNameAsync(string userName)
        {
            var user = await _context.Users.FirstOrDefaultAsync(user => user.UserName == userName);
            return user; 
        }
    }
}
