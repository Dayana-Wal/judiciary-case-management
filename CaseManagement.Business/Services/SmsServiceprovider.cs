using Microsoft.Extensions.Options;
using Twilio.Types;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using CaseManagement.Business.Common;
using System;
using System.Threading.Tasks;

namespace CaseManagement.Business.Services
{
    public class SmsServiceprovider
    {
        private readonly TwilioSettings _twilioSettings;
        public SmsServiceprovider(IOptions<TwilioSettings> twilioSettings)
        {
            _twilioSettings = twilioSettings.Value;

            // Initialize Twilio client
            TwilioClient.Init(_twilioSettings.AccountSid, _twilioSettings.AuthToken);
        }

        public async Task<OperationResult> SendSms(string toPhoneNumber, string messageBody)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(toPhoneNumber) || string.IsNullOrWhiteSpace(messageBody))
                {
                    return new OperationResult
                    {
                        Status = "ERROR",
                        Message = "Recipient phone number and message body cannot be null or empty."
                    };
                }
                // Send SMS via Twilio
                var message = await MessageResource.CreateAsync(
                    body: messageBody,
                    from: new PhoneNumber(_twilioSettings.PhoneNumber),
                    to: new PhoneNumber(toPhoneNumber)
                );

                // Return success response
                return new OperationResult
                {
                    Status = "SUCCESS",
                    Message = "SMS sent successfully."
                };
            }
            catch (Exception ex)
            {
                return new OperationResult
                {
                    Status = "ERROR",
                    Message = $"Failed to send SMS: {ex.Message}"
                };
            }
        }
    }
}
