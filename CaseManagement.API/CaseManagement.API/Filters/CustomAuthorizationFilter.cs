
using CaseManagement.Business.Common;
using CaseManagement.Business.Queries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System.Security.Claims;

namespace CaseManagement.API.Filters
{
    public class CustomAuthorizationFilter : Attribute, IAsyncAuthorizationFilter
    {
        private readonly string[] _allowedRoles;
        private readonly IPersonQueryHandler _personQueryHandler;
        public CustomAuthorizationFilter(string[] allowedRoles, IPersonQueryHandler personQueryHandler)
        {
            _allowedRoles = allowedRoles;
            _personQueryHandler = personQueryHandler;
        }
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var userClaims = context.HttpContext.User;
            if (userClaims?.Identity?.IsAuthenticated != true) {
                var op = OperationResult.Failed("User is not authenticated. Please log in.");
                context.Result = new JsonResult(op) { StatusCode = 401 };
                return;
            }

            var userName = userClaims.FindFirst(ClaimTypes.Name)?.Value;
            var role = userClaims.FindFirst(ClaimTypes.Role)?.Value;

            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(role)) {
                var op = OperationResult.Failed("Missing required claims, UserName or Role is not included in the token");
                context.Result = new JsonResult(op) { StatusCode = 401};
                return;
            }

            var user = await _personQueryHandler.GetUserAsync(userName);

            if (user == null || !_allowedRoles.Contains(role, StringComparer.OrdinalIgnoreCase)) { 
                var op = OperationResult.Failed($"Access denied. The role '{role}' is not authorized to access this resource.");
                context.Result = new JsonResult(op) { StatusCode = 403 };
                return;
            }

        }
    }

    public class CustomAuthorizationAttribute : TypeFilterAttribute
    {
        public CustomAuthorizationAttribute(params string[] allowedRole)
            : base(typeof(CustomAuthorizationFilter))
        {
            Arguments = new object[] { allowedRole };
        }
    }
}
