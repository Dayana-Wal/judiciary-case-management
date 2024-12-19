
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

                try
                {
                    var existingPerson = await _context.People
                        .FirstOrDefaultAsync(p => p.Email == person.Email);

                    if (existingPerson != null)
                    {
                        return new OperationResult<string> { Status=OperationStatus.Failed, Message= $"Person with this email already exists", Data= person.Email};
                    
                    }

                    await _context.People.AddAsync(person);

                    var existingUser = await _context.Users
                        .FirstOrDefaultAsync(u => u.UserName == user.UserName);

                    if (existingUser != null)
                    {
                        return new OperationResult<string> { Status = OperationStatus.Failed, Message = $"User with this username already exists" , Data=  user.UserName };
                    }

                    await _context.Users.AddAsync(user);

                    await _context.SaveChangesAsync();

                    return new OperationResult<string> { Status = OperationStatus.Success, Message = $"Details stored successfully!" };

                }
                catch (Exception ex)
                {
                    return new OperationResult<string> { Status = OperationStatus.Failed, Message = $"An Exception Occured while storing data to database" , Data =  ex.Message };

                }
            }
        }

        public Task DeleteAsync<T>(Person person)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync<T>(Person person)
        {
            throw new NotImplementedException();
        }

        
    }
}
