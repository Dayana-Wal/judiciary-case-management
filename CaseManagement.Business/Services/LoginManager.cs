using CaseManagement.Business.Common;
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
        public LoginManager(JwtTokenProvider jwtTokenProvider, PasswordService passwordService)
        {
            _jwtTokenProvider = jwtTokenProvider;
            _passwordService = passwordService;
        }
        public OperationResult UserLogin(string username, string password)
        {
            //TODO: Get the salt and password  from user table
            string salt = "0LtwYy1/kufYqKLdBSNNpA==";
            string passwordFromDb = "oXnNr85vuAxfDP1YlZ/r+pq1BqpLYn4Uw/ak4PopLcA=";

            bool isPasswordMatched = _passwordService.VerifyEnteredPassword(password, passwordFromDb, salt);
            if(isPasswordMatched)
            {
                string token = _jwtTokenProvider.GenerateJwtToken(username, password);
                Console.WriteLine(token);
                //TODO: Pass the token as result
                return new OperationResult { Status = "Success", Message = "Login Success" };

            }

            else
            {
                return new OperationResult { Status = "Failed", Message = "Incorrect Password" };
            }

            //Fetch user details(PasswordHash,PasswordSalt,RoleId) based on username --> data access
            //Generate HashedPassword using password and salt --> can use the function in signup
            //Compare HashedPassword and PasswordHash
            //If same generate JWT token
            //If not same send "Incorrect password" message
        }
      
    }
}
