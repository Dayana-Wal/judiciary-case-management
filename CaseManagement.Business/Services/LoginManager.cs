using CaseManagement.Business.Common;
using CaseManagement.DataAccess.Entities;
using CaseManagement.DataAccess.Queries;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CaseManagement.Business.Services
{
    public class LoginManager
    {
        private readonly JwtTokenProvider _jwtTokenProvider;
        private readonly PasswordService _passwordService;
        private readonly PersonQueryHandler _personQueryHandler;
        public LoginManager(JwtTokenProvider jwtTokenProvider, PasswordService passwordService, PersonQueryHandler personQueryHandler)
        {
            _jwtTokenProvider = jwtTokenProvider;
            _passwordService = passwordService;
            _personQueryHandler = personQueryHandler;
        }
        public async Task<OperationResult<string>> UserLogin(string username, string password)
        {
            //Get the user data from user table
            User user = await _personQueryHandler.GetUserAsync(username);
            if (user == null)
            {
                //return new OperationResult<string>
                //{
                //    Status = "Failed",
                //    Message = "User not found with the provided username"
                //};
                throw new UnauthorizedAccessException("User not found with the provided username");

            }
            bool isPasswordMatched = _passwordService.VerifyEnteredPassword(password, user.PasswordHash, user.PasswordSalt);
            if (isPasswordMatched)
            {
                string token = _jwtTokenProvider.GenerateJwtToken(username, password);
                return new OperationResult<string> { Status = "Success", Message = "Login Success", Data = token };

            }
            else
            {
                //return new OperationResult<string>
                //{
                //    Status = "Failed",
                //    Message = "Incorrect password provided"
                //};
                throw new UnauthorizedAccessException("Incorrect Password");
            }

        }

    }
}
