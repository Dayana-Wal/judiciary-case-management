
using CaseManagement.Business.Common;
using CaseManagement.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;


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
                        return OperationResult<string>.Failed($"Person with this email already exists", person.Email);
                        //return new OperationResultT<string> { Status="Failed", Message= $"Person with this email already exists", Data= person.Email};
                    
                    }

                    await _context.People.AddAsync(person);

                    var existingUser = await _context.Users
                        .FirstOrDefaultAsync(u => u.UserName == user.UserName);

                    if (existingUser != null)
                    {
                        return OperationResult<string>.Failed($"User with this username already exists", user.UserName);

                        //return new OperationResultT<string> { Status = "Failed", Message = $"User with this username already exists" , Data=  user.UserName };
                    }

                    await _context.Users.AddAsync(user);

                    await _context.SaveChangesAsync();

                    return OperationResult<string>.Success("Details stored successfully!");

                    //$"User with this username already exists" , Data=  user.UserName 
                    //return new OperationResultT<string> { Status = "Success", Message = $"Details stored successfully!" };

                }
                catch (Exception ex)
                {
                    return OperationResult<string>.Failed("An Exception Occured while storing data to database", ex.Message);

                    //return new OperationResultT<string> { Status = "Failed", Message = $"An Exception Occured while storing data to database" , Data =  ex.Message };

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
