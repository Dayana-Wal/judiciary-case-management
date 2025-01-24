using CaseManagement.Business.Common;
using CaseManagement.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;


namespace CaseManagement.Business.Commands
{
    public class RoleUpdateCommandHandler : IRoleUpdateCommandHandler
    {
        private readonly CaseManagementContext _context;

        public RoleUpdateCommandHandler(CaseManagementContext context)
        {
            _context = context;
        }

        public async Task<User?> GetUserByIdAsync(string userId)
        {
            return await _context.Users
                .Include(u => u.Role) 
                .FirstOrDefaultAsync(u => u.Id == userId);  
        }

        public async Task<OperationResult> UpdateUserAsync(User user)
        {

            if (user == null)
            {
                return OperationResult.Failed(message: "User cannot be null.");
            }

            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return OperationResult.Success("User updated successfully.");
            
        }
    }
}
