﻿
using CaseManagement.DataAccess.Entities;
using CaseManagement.DataAccess.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseManagement.DataAccess.Commands
{
    public interface IPersonCommandHandler
    {
        //Task<bool> CreatePersonAsync(Person person);
        //Task<bool> CreateUserAsync(User user);

        Task<bool> CreatePersonAndUserAsync(Person person , User user);

        Task UpdateAsync<T>(Person person);

        Task DeleteAsync<T>(Person person);

        //Task<(List<Person> persons, List<User> users)> GetAllPersonsAndUsersAsync();
    }
}
