using CaseManagement.Business.Common;
using CaseManagement.Business.Features.Otp;
using CaseManagement.Business.Service;
using Microsoft.AspNetCore.Mvc;

namespace CaseManagement.API.Controllers
{
    public class OtpController : BaseController
    {
        private readonly OtpManager _otpManager;

        public OtpController(OtpManager otpManager)
        {
            _otpManager = otpManager;
        }

        [HttpPost("generate")]
        public async Task<IActionResult> GenerateOtp([FromForm] string requestedBy, [FromForm] string phoneNumber, [FromForm] string usedForCode)
        {
            var otpCommand = new OtpCommand
            {
                UserId = requestedBy,
                PhoneNumber = phoneNumber,
                UsedForCode = usedForCode
            };

            OtpCommandValidator validator = new OtpCommandValidator();
            var validationResult = validator.Validate(otpCommand);

            var otpResult = new OperationResult();

            if (validationResult.IsValid)
            {
                try
                {
                    var otp = await _otpManager.StoreOtp(requestedBy, phoneNumber, usedForCode);

                    otpResult.Status = OperationStatus.Success;
                    otpResult.Message = otp.IsVerified ? "OTP has already been verified. No new OTP sent." : "OTP sent successfully";

                    return ToResponse(otpResult);
                }
                catch (Exception ex)
                {
                    otpResult.Status = OperationStatus.Failed;
                    otpResult.Message = "OTP sending failed due to an exception.";
                    return StatusCode(500, otpResult);
                }
            }
            else
            {
                // Collect validation errors
                List<string> validationErrors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();

                otpResult.Status = OperationStatus.Failed;
                otpResult.Message = "Validation failed";
                var returnResponse = OperationResultConverter.ConvertTo(otpResult, validationErrors);
                return ToResponse(returnResponse);
            }
        }

        [HttpPost("verify")]
        public async Task<IActionResult> VerifyOtp([FromForm] string userId, [FromForm] string otp)
        {
            var result = await _otpManager.VerifyOtp(userId, otp);
            return ToResponse(result);
        }
    }
}



