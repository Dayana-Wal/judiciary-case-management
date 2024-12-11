using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twilio.Types;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace CaseManagement.Business.Services
{
    public class SmsServiceprovider
    {
        private readonly string _accountSid;
        private readonly string _authToken;
        private readonly string _twilioPhoneNumber;

        public SmsServiceprovider(IConfiguration configuration)
        {
            _accountSid = configuration["Twilio:AccountSid"];
            _authToken = configuration["Twilio:AuthToken"];
            _twilioPhoneNumber = configuration["Twilio:PhoneNumber"];

            // Initialize Twilio client
            TwilioClient.Init(_accountSid, _authToken);
        }

        public void SendSms(string toPhoneNumber, string messageBody)
        {
            if (string.IsNullOrWhiteSpace(toPhoneNumber))
                throw new ArgumentException("Recipient phone number cannot be null or empty.");

            if (string.IsNullOrWhiteSpace(messageBody))
                throw new ArgumentException("Message body cannot be null or empty.");

            // Send SMS using Twilio
            var message = MessageResource.Create(
                body: messageBody,
                from: new PhoneNumber(_twilioPhoneNumber),
                to: new PhoneNumber(toPhoneNumber)
            );

        }
    }
}
