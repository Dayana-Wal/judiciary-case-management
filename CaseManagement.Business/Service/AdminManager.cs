

using CaseManagement.Business.Common;
using CaseManagement.Business.Features.Admin;
using CaseManagement.Business.Queries;
using CaseManagement.DataAccess.DTO;

namespace CaseManagement.Business.Service
{
    public class AdminManager : BaseManager
    {
        private readonly IAdminQueryHandler _adminHandler;

        public AdminManager(IAdminQueryHandler adminHandler)
        {
            _adminHandler = adminHandler;
        }

        public async Task<OperationResult<PagedList<UsersDto>>> GetUsers(UsersQuery usersQuery)
        {
            var data = await  _adminHandler.GetUsersAsync(usersQuery);
            return OperationResult < PagedList < UsersDto >>.Success(data: data);
        }
    }
}
