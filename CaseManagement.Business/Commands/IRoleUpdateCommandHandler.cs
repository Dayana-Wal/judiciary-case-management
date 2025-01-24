using CaseManagement.Business.Common;
using CaseManagement.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseManagement.Business.Commands
{
    public interface IRoleUpdateCommandHandler
    {
        Task<User?> GetUserByIdAsync(string userId);
        Task<OperationResult> UpdateUserAsync(User user);
    }
}
