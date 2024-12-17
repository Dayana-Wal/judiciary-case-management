
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



        public async Task<string> CreateUserAsync(Person person, User user)
        {
            {

                try
                {
                    var existingPerson = await _context.People
                        .FirstOrDefaultAsync(p => p.Email == person.Email);

                    if (existingPerson != null)
                    {
                        //return new OperationResult { Status="Failed", Message= $"Person with {person.Email} already exists, cannot proceed" };
                        return "Person with given email id already exists";
                    }

                    await _context.People.AddAsync(person);

                    var existingUser = await _context.Users
                        .FirstOrDefaultAsync(u => u.UserName == user.UserName);

                    if (existingUser != null)
                    {
                        return "User with given user name already exists";
                        //return new OperationResult { Status = "Failed", Message = $"User with {user.UserName} already exists, cannot proceed" };
                    }

                    await _context.Users.AddAsync(user);

                    await _context.SaveChangesAsync();

                    //return new OperationResult { Status = "Success", Message = $"Person and User Details stored successfully!" };

                    return "Success";
                }
                catch (Exception ex)
                {
                    //return new OperationResult { Status = "Failed", Message = $"Exception Occured: {ex.Message}" };

                    return $"Exception occured : {ex.Message}";
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
