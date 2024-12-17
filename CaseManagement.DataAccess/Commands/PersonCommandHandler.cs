
using CaseManagement.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace CaseManagement.DataAccess.Commands
{
    public class PersonCommandHandler : IPersonCommandHandler
    {
        private readonly CaseManagementContext _context = new CaseManagementContext();

        public PersonCommandHandler()
        {
        }

        public async Task<bool> CreatePersonAndUserAsync(Person person, User user)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {

                try
                {
                    var existingPerson = await _context.People
                        .FirstOrDefaultAsync(p => p.Email == person.Email);

                    if (existingPerson != null)
                    {
                        await transaction.RollbackAsync();
                        //return new OperationResult { Status="Failed", Message= $"Person with {person.Email} already exists, cannot proceed" };
                        return false;
                    }

                    await _context.People.AddAsync(person);

                    var existingUser = await _context.Users
                        .FirstOrDefaultAsync(u => u.UserName == user.UserName);

                    if (existingUser != null)
                    {
                        await transaction.RollbackAsync();
                        return false;
                        //return new OperationResult { Status = "Failed", Message = $"User with {user.UserName} already exists, cannot proceed" };
                    }

                    await _context.Users.AddAsync(user);

                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();

                    //return new OperationResult { Status = "Success", Message = $"Person and User Details stored successfully!" };

                    return true;
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    //return new OperationResult { Status = "Failed", Message = $"Exception Occured: {ex.Message}" };

                    return false;
                }
            }
        }

        


        //public async Task<bool> CreatePersonAsync(Person person)
        //{
        //    var existingPerson = _context.People.
        //        FirstOrDefault(p => p.Email == person.Email);

        //    if (existingPerson != null)
        //    {
        //        return false;
        //    }

        //    await _context.People.AddAsync(person);

        //    await _context.SaveChangesAsync();

        //    return true;
        //}

        public Task DeleteAsync<T>(Person person)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync<T>(Person person)
        {
            throw new NotImplementedException();
        }

        //public async Task<bool> CreateUserAsync(User user)
        //{
        //    var existingUser = _context.Users.
        //        FirstOrDefault(u => u.UserName == user.UserName);

        //    if (existingUser != null)
        //    {
        //        return false;
        //    }

        //    await _context.Users.AddAsync(user);

        //    await _context.SaveChangesAsync();

        //    return true;
        //}
    }
}
