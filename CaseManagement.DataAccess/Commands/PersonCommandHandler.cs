
using CaseManagement.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseManagement.DataAccess.Commands
{
    public class PersonCommandHandler : IPersonCommandHandler
    {
        private readonly CaseManagementContext _context = new CaseManagementContext();

        public PersonCommandHandler()
        {
        }

        public async Task<bool> CreatePersonAsync(Person person)
        {
            var existingPerson = _context.People.
                FirstOrDefault(p => p.Email == person.Email);

            if (existingPerson != null)
            {
                return false;
            }

            await _context.People.AddAsync(person);

            await _context.SaveChangesAsync();

            return true;
        }

        public Task DeleteAsync<T>(Person person)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync<T>(Person person)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> CreateUserAsync(User user)
        {
            var existingUser = _context.Users.
                FirstOrDefault(u => u.UserName == user.UserName);

            if (existingUser != null)
            {
                return false;
            }

            await _context.Users.AddAsync(user);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
