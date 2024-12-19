using CaseManagement.Business.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CaseManagement.Business.Providers
{
    public class OtpProvider
    {
        private readonly SmsServiceprovider _smsServiceProvider;

        public OtpProvider(SmsServiceprovider smsServiceprovider)
        {
            _smsServiceProvider = smsServiceprovider;
        }
        public static string GenerateOtpValue()
        {
            Random rand = new Random();
            return rand.Next(100000, 999999).ToString();
        }

        public static string HashOtp(string otpValue)
        {
            if (string.IsNullOrWhiteSpace(otpValue))
            {
                return "Otp value can't be null or empty";
            }

            using (SHA256 sha256 = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(otpValue);
                var hashBytes = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hashBytes);
            }
        }

        public static (string otpValue, string otpHash) GenerateAndHashOtp()
        {
            string otpValue = GenerateOtpValue();
            string otpHash = HashOtp(otpValue);
            return (otpValue, otpHash);
        }

        public async Task SendOtp(string phoneNumber, string otpValue)
        {
            var message = $"Your OTP code is: {otpValue}";
            await _smsServiceProvider.SendSms(phoneNumber, message);
        }

    }
}
