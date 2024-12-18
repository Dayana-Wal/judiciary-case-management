using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CaseManagement.Business.Utility
{

    public class PasswordSaltHashResult
    {
        public string HashedPassword { get; set; }
        public string Salt { get; set; }
    }

    public class HashHelper
    {
        public PasswordSaltHashResult HashedResult(string password)
        {
            string saltValue = GenerateSalt();
            string hashedPasswordSalt = HashPassword(password, saltValue);
            return new PasswordSaltHashResult { HashedPassword = hashedPasswordSalt, Salt = saltValue };
        }

        public string GenerateSalt()
        {
            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                byte[] saltBytes = new byte[16];
                rng.GetBytes(saltBytes);
                return Convert.ToBase64String(saltBytes);
            }
        }

        //hashing the password with salt included
        public string HashPassword(string password, string salt)
        {
            string saltedPassword = password + salt;
            byte[] passwordBytes = Encoding.UTF8.GetBytes(saltedPassword);

            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(passwordBytes);
                return Convert.ToBase64String(hashBytes);
            }
        }

        public bool VerifyEnteredPassword(string enteredPassword, string storedHashedPassword, string storedSalt)
        {
            string enteredHashedPassword = HashPassword(enteredPassword, storedSalt);
            return enteredHashedPassword == storedHashedPassword;
        }
    }
}