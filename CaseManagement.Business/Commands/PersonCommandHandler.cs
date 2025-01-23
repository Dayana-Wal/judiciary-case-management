
using CaseManagement.Business.Common;
using CaseManagement.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace CaseManagement.DataAccess.Commands
{
    public class PersonCommandHandler : IPersonCommandHandler
    {
        private readonly CaseManagementContext _context;

        public PersonCommandHandler(CaseManagementContext context)
        {
            _context = context;
        }

        public async Task<OperationResult<string>> CreateUserAsync(Person person, User user)
        {
            {

                var existingPerson = await _context.People
                    .FirstOrDefaultAsync(p => p.Email == person.Email);

            if (existingPerson != null)
            {
                return OperationResult<string>.Failed($"Person with this email: {person.Email} already exists");                    
            }

                await _context.People.AddAsync(person);

            var existingUser = await _context.Users
                .FirstOrDefaultAsync(u => u.UserName.ToLower() == user.UserName.ToLower());

            if (existingUser != null)
            {
                return OperationResult<string>.Failed($"User with this username: {user.UserName} already exists");
            }

                await _context.Users.AddAsync(user);

                await _context.SaveChangesAsync();

                return OperationResult<string>.Success("Details stored successfully!");

        }

        }

    }

}

        
 
