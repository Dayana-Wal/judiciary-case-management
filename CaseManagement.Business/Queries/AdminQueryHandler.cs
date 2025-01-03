using CaseManagement.Business.Features.Admin;
using CaseManagement.DataAccess.DTO;
using CaseManagement.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace CaseManagement.Business.Queries
{
    public class AdminQueryHandler : IAdminQueryHandler
    {
        private readonly CaseManagementContext _context;

        public AdminQueryHandler(CaseManagementContext context)
        {
            _context = context;
        }

        public async Task<PagedList<UsersDto>> GetUsersAsync(UsersQuery usersQuery)
        {
            
                var query = _context.Users
                    .Include(u => u.Role)
                    .Include(u => u.Person)
                    .AsQueryable();

                // Sorting
                if (!string.IsNullOrEmpty(usersQuery.SortBy))
                {
                    switch (usersQuery.SortBy.ToLower())
                    {
                        case "username":
                            query = usersQuery.SortDirection.ToLower() == "asc"
                                ? query.OrderBy(u => u.UserName)
                                : query.OrderByDescending(u => u.UserName);
                            break;
                        default:
                            query = usersQuery.SortDirection.ToLower() == "asc"
                                ? query.OrderBy(u => u.Id)
                                : query.OrderByDescending(u => u.Id);
                            break;
                    }
                }

                var totalUsers = await query.CountAsync();

            var users = await query
                .Skip((usersQuery.PageNumber - 1) * usersQuery.PageSize)
                .Take(usersQuery.PageSize)
                .Select(u => new UsersDto
                {
                    Id = u.Id,
                    UserName = u.UserName,
                    Role = u.Role.Text,
                    Name = u.Person.Name

                })
                .ToListAsync();
            var response = new PagedList<UsersDto>(items:users, totalCount:totalUsers, pageNumber:usersQuery.PageNumber, pageSize:usersQuery.PageSize);

            return response;
            }
    }
}
