using CaseManagement.Business.Common;
using CaseManagement.Business.Features.Login;
using CaseManagement.Business.Providers;
using CaseManagement.Business.Queries;
using CaseManagement.Business.Utility;
using CaseManagement.DataAccess.Entities;

namespace CaseManagement.Business.Service
{
    public class LoginManager
    {
        private readonly JwtTokenProvider _jwtTokenProvider;
        private readonly HashHelper _hashHelper;
        private readonly IPersonQueryHandler _personQueryHandler;
        public LoginManager(JwtTokenProvider jwtTokenProvider, HashHelper hashHelper, IPersonQueryHandler personQueryHandler)
        {
            _jwtTokenProvider = jwtTokenProvider;
            _hashHelper = hashHelper;
            _personQueryHandler = personQueryHandler;
        }
        public async Task<OperationResult<object>> UserLogin(LoginQuery loginQuery)
        {
            //Get the user data from user table
            User user = await _personQueryHandler.GetUserAsync(loginQuery.UserName);
            if (user == null)
            {
                return OperationResult<object>.Failed(null, "User not found with the provided username");

            }
            bool isPasswordMatched = _hashHelper.VerifyEnteredPassword(loginQuery.Password, user.PasswordHash, user.PasswordSalt);
            if (isPasswordMatched)
            {
                string token = _jwtTokenProvider.GenerateJwtToken(loginQuery.UserName, user.Role.Text);
                var response = new
                {
                    Token = token,
                    User = new
                    {
                        Id = user.Id,
                        UserName = user.UserName,
                        Role = user.Role.Text
                    }
                };
                return OperationResult<object>.Success(response, "Login success");
            }
            else
            {
                return OperationResult<object>.Failed(null, "Incorrect password provided");
            }

        }

    }
}
