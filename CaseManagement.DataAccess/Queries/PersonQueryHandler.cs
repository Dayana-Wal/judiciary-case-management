


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

        Task<Person> IPersonQueryHandler.GetPersonByEmailAsync { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Task<Person> GetPersonByNameAsync { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Task<User> GetUserByEmailAsync { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

       

       

    }
}
