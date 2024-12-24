
using CaseManagement.Business.Queries;
using CaseManagement.DataAccess.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace CaseManagement.API.Filters
{
    public class CustomAuthorizationFilter : Attribute, IAsyncAuthorizationFilter
    {
        private readonly string _requiredRole;
        private readonly IPersonQueryHandler _personQueryHandler;
        public CustomAuthorizationFilter(string requiredRole, IPersonQueryHandler personQueryHandler)
        {
            _requiredRole = requiredRole;
            _personQueryHandler = personQueryHandler;
        }
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var userClaims = context.HttpContext.User;
            if (userClaims?.Identity?.IsAuthenticated != true) {
                context.Result = new UnauthorizedResult();
                return;
            }
            //User.FindFirst(ClaimTypes.Role)?.Value
            var userName = userClaims.FindFirst(ClaimTypes.Name)?.Value;
            var role = userClaims.FindFirst(ClaimTypes.Role)?.Value;

            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(role)) {
                context.Result = new UnauthorizedResult();
                return;
            }

            var user = await _personQueryHandler.GetUserAsync(userName);

            if (user == null || !string.Equals(role, _requiredRole, StringComparison.OrdinalIgnoreCase)) { 
                context.Result = new ForbidResult();
            }

        }
    }

    public class CustomAuthorizationAttribute : TypeFilterAttribute
    {
        public CustomAuthorizationAttribute(string requiredRole)
            : base(typeof(CustomAuthorizationFilter))
        {
            Arguments = new object[] { requiredRole };
        }
    }
}
