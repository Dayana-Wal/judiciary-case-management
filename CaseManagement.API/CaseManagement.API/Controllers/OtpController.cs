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
        public async Task<IActionResult> GenerateOtp([FromBody] OtpGenerateCommand otpCommand)
        {
            var validationResult = otpCommand.ValidateCommand();

            var otpResult = new OperationResult();

            if (validationResult.IsValid)
            {
                try
                {
                    var otp = await _otpManager.StoreOtp(otpCommand.UserId, otpCommand.PhoneNumber, otpCommand.UsedForCode);

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
        public async Task<IActionResult> VerifyOtp([FromBody] OtpVerifyCommand otpCommand)
        {
            var validationResult = otpCommand.ValidateCommand();

            var result = new OperationResult();

            if (validationResult.IsValid)
            {
                try
                {
                    var verificationResult = await _otpManager.VerifyOtp(otpCommand.UserId, otpCommand.Otp);
                    return ToResponse(verificationResult);
                }
                catch (Exception ex)
                {
                    result.Status = OperationStatus.Failed;
                    result.Message = "OTP verification failed due to an exception.";
                    return StatusCode(500, result);
                }
            }
            else
            {
                // Collect validation errors
                List<string> validationErrors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();

                result.Status = OperationStatus.Failed;
                result.Message = "Validation failed";
                var returnResponse = OperationResultConverter.ConvertTo(result, validationErrors);
                return ToResponse(returnResponse);
            }
        }
    }
}



