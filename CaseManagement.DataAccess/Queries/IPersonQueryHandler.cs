
using CaseManagement.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseManagement.DataAccess.Queries
{
    public interface IPersonQueryHandler
    {
        Task<User> GetUserAsync(string userName);
    }
}
