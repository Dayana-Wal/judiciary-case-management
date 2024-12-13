using Microsoft.Extensions.Configuration;
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
    public class LoginService
    {     
        public string UserLogin(string username, string password)
        {
            string token = GenerateJwtToken(username, "Admin");
            Console.WriteLine(token);
            return token;
            //Fetch user details(PasswordHash,PasswordSalt,RoleId) based on username --> data access
            //Generate HashedPassword using password and salt --> can use the function in signup
            //Compare HashedPassword and PasswordHash
            //If same generate JWT token
            //If not same send "Incorrect password" message
        }

        public bool VerifyPassword(string password, string PasswordHash) {
            return password == PasswordHash;
        }


        private string GenerateJwtToken(string userName, string role)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, userName),
                new Claim(JwtRegisteredClaimNames.Sub, userName),
                new Claim(JwtRegisteredClaimNames.Jti, Ulid.NewUlid().ToString()),
                new Claim(ClaimTypes.Role,role )
            };
            //TODO --> Get the values from configuration file
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SuperStrongSecretKey123!SuperStrongSecretKey123!"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            
            var token = new JwtSecurityToken(
                issuer:"Issuer",
                audience:"Audience",
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds
                );

            return new JwtSecurityTokenHandler().WriteToken(token);

        }

    }
}
