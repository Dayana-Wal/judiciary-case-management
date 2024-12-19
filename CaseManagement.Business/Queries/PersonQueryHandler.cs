using CaseManagement.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace CaseManagement.Business.Queries
{
    public class PersonQueryHandler : IPersonQueryHandler
    {
        private readonly CaseManagementContext _context;

        public PersonQueryHandler(CaseManagementContext context)
        {
            _context = context;
        }

        public async Task<User> GetUserAsync(string userName)
        {
            var user = await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(user => user.UserName == userName);
            return user;

        }

    }
}
