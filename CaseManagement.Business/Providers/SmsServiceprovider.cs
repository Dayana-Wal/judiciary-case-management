using CaseManagement.Business.Common;
using Microsoft.Extensions.Options;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

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
                    return OperationResult.Failed(message: "Recipient phone number and message body cannot be null or empty.");
                }
                // Send SMS via Twilio
                var message = await MessageResource.CreateAsync(
                    body: messageBody,
                    from: new PhoneNumber(_twilioSettings.PhoneNumber),
                    to: new PhoneNumber(toPhoneNumber)
                );

                return OperationResult.Success(message: "SMS sent successfully.");

            }
            catch (Exception ex)
            {
                return OperationResult.Failed(message: $"Failed to send SMS: {ex.Message}");
            }
        }

    }
}
