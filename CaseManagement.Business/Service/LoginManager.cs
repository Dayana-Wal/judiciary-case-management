using CaseManagement.Business.Common;
using CaseManagement.Business.Features.Login;
using CaseManagement.Business.Queries;
using CaseManagement.Business.Utility;
using CaseManagement.DataAccess.Entities;

namespace CaseManagement.Business.Service
{
    public class LoginManager
    {
        private readonly JwtTokenProvider _jwtTokenProvider;
        private readonly HashHelper _hashHelper;
        private readonly PersonQueryHandler _personQueryHandler;
        public LoginManager(JwtTokenProvider jwtTokenProvider, HashHelper hashHelper, PersonQueryHandler personQueryHandler)
        {
            _jwtTokenProvider = jwtTokenProvider;
            _hashHelper = hashHelper;
            _personQueryHandler = personQueryHandler;
        }
        public async Task<OperationResult<string>> UserLogin(LoginQuery loginQuery)
        {
            //Get the user data from user table
            User user = await _personQueryHandler.GetUserAsync(loginQuery.UserName);
            if (user == null)
            {
                return new OperationResult<string>
                {
                    Status = OperationStatus.Error,
                    Message = "User not found with the provided username"
                };

            }
            bool isPasswordMatched = _hashHelper.VerifyEnteredPassword(loginQuery.Password, user.PasswordHash, user.PasswordSalt);
            if (isPasswordMatched)
            {
                string token = _jwtTokenProvider.GenerateJwtToken(loginQuery.UserName, user.Role.Text);
                return new OperationResult<string> { Status = OperationStatus.Success, Message = "Login Success", Data = token };

            }
            else
            {
                return new OperationResult<string>
                {
                    Status = OperationStatus.Error,
                    Message = "Incorrect password provided"
                };
            }

        }

    }
}
