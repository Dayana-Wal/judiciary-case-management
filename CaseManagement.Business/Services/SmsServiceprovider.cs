using Microsoft.Extensions.Configuration;
using System;
using Twilio.Types;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using CaseManagement.Business.Common;

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

        public OperationResult SendSms(string toPhoneNumber, string messageBody)
        {
            if (string.IsNullOrWhiteSpace(toPhoneNumber))
            {
                return new OperationResult
                {
                    Status = "ERROR",
                    Msg = "Recipient phone number cannot be null or empty."
                };
            }

            if (string.IsNullOrWhiteSpace(messageBody))
            {
                return new OperationResult
                {
                    Status = "ERROR",
                    Msg = "Message body cannot be null or empty."
                };
            }

            try
            {
                var message = MessageResource.Create(
                    body: messageBody,
                    from: new PhoneNumber(_twilioPhoneNumber),
                    to: new PhoneNumber(toPhoneNumber)
                );

                // Return success response
                return new OperationResult
                {
                    Status = "SUCCESS",
                    Msg = "SMS sent successfully."
                };
            }
            catch (Exception ex)
            {
                return new OperationResult
                {
                    Status = "ERROR",
                    Msg = $"Failed to send SMS: {ex.Message}"
                };
            }
        }
    }
}
