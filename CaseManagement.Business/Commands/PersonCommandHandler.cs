
using CaseManagement.Business.Common;
using CaseManagement.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
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
                        return new OperationResult<string> { Status="Failed", Message= $"Person with this email already exists", Data= person.Email};
                        //return "Person with given email id already exists";
                    }

                    await _context.People.AddAsync(person);

                    var existingUser = await _context.Users
                        .FirstOrDefaultAsync(u => u.UserName == user.UserName);

                    if (existingUser != null)
                    {
                        //return "User with given user name already exists";
                        return new OperationResult<string> { Status = "Failed", Message = $"User with this username already exists" , Data=  user.UserName };
                    }

                    await _context.Users.AddAsync(user);

                    await _context.SaveChangesAsync();

                    return new OperationResult<string> { Status = "Success", Message = $"Details stored successfully!" };

                    //return "Success";
                }
                catch (Exception ex)
                {
                    return new OperationResult<string> { Status = "Failed", Message = $"An Exception Occured while storing data to database" , Data =  ex.Message };

                    //return $"Exception occured : {ex.Message}";
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
