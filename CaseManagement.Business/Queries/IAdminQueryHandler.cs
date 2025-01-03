using CaseManagement.Business.Features.Admin;
using CaseManagement.DataAccess.DTO;

namespace CaseManagement.Business.Queries
{
    public interface IAdminQueryHandler
    {
        Task<PagedList<UsersDto>> GetUsersAsync(UsersQuery usersQuery);
    }
}
