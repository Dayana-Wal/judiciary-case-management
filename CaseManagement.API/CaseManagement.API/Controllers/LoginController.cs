using CaseManagement.Business.Common;
using CaseManagement.Business.Features.Login;
using CaseManagement.Business.Service;
using Microsoft.AspNetCore.Mvc;

namespace CaseManagement.API.Controllers
{
    public class LoginController : BaseController
    {
        private readonly LoginManager _loginManager;

        public LoginController(LoginManager loginManager)
        {
            _loginManager = loginManager;
        }
        [HttpPost("user")]
        public async Task<IActionResult> Login([FromBody] LoginQuery loginQuery)
        {
            if (loginQuery == null)
            {
                return ToResponse(OperationResult.Failed("Invalid user data"));
            }

            var validationResult = loginQuery.ValidateQuery();
            if (!validationResult.IsValid)
            {
                var validationErrors = new List<string>();
                validationErrors.AddRange(validationResult.Errors.Select(err => err.ToString()));
                var result = OperationResult<List<string>>.ValidationError(validationErrors);
                return ToResponse<List<string>>(result);

            }

            var opresult = await _loginManager.UserLogin(loginQuery);
            return ToResponse<string>(opresult);

        }

    }
}
