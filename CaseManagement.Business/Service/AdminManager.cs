

using CaseManagement.Business.Commands;
using CaseManagement.Business.Common;
using CaseManagement.Business.Features.Admin;
using CaseManagement.Business.Queries;
using CaseManagement.DataAccess.DTO;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace CaseManagement.Business.Service
{
    public class AdminManager : BaseManager
    {
        private readonly IAdminQueryHandler _adminHandler;
        private readonly IRoleUpdateCommandHandler _roleUpdateCommandHandler;
        private readonly ILookUpConstantsQuery _lookUpConstantsQuery;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AdminManager(IAdminQueryHandler adminHandler, IRoleUpdateCommandHandler roleUpdateCommandHandler, ILookUpConstantsQuery lookUpConstantsQuery, IHttpContextAccessor httpContextAccessor)
        {
            _adminHandler = adminHandler;
            _roleUpdateCommandHandler = roleUpdateCommandHandler;
            _lookUpConstantsQuery = lookUpConstantsQuery;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<OperationResult<PagedList<UsersDto>>> GetUsers(UsersQuery usersQuery)
        {
            var data = await  _adminHandler.GetUsersAsync(usersQuery);
            return OperationResult < PagedList < UsersDto >>.Success(data: data);
        }

        private bool IsUserAdmin()
        {
            var userRoleClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Role)?.Value;
            return userRoleClaim != null && userRoleClaim == "Admin";
        }

        public async Task<OperationResult> UpdateUserRoleAsync(string userId, string roleName)
        {
            //Ensure the current user is an Admin
            if (!IsUserAdmin())
            {
                return OperationResult.Failed(message: "You do not have permission to update user roles.");
            }

            var roleId = await _lookUpConstantsQuery.GetConstantId(roleName, "User Role");
            if (roleId == 0)
            {
                return OperationResult.Failed(message: "Invalid role name provided.");
            }

            var user = await _roleUpdateCommandHandler.GetUserByIdAsync(userId);
            if (user == null)
            {
                return OperationResult.Failed(message: "User not found.");
            }

            user.RoleId = roleId;
            return await _roleUpdateCommandHandler.UpdateUserAsync(user);
        }
    }
}
