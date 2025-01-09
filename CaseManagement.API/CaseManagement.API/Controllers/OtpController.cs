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

            var otpOperationResult = new OperationResult();
            if (validationResult.IsValid)
            {
                // Generate OTP and store it
                var otpResult = await _otpManager.StoreOtp(otpCommand.UserId, otpCommand.PhoneNumber, otpCommand.UsedForCode);

                if (otpResult.Status == OperationStatus.Success && otpResult.Data != null)
                {
                    var otp = otpResult.Data;

                    otpOperationResult.Status = OperationStatus.Success;
                    otpOperationResult.Message = otp.IsVerified ? "OTP has already been verified. No new OTP sent." : "OTP sent successfully";
                }
                else
                {
                    otpOperationResult.Status = OperationStatus.Failed;
                    otpOperationResult.Message = "OTP generation failed.";
                }

                return ToResponse(otpOperationResult);
            }
            else
            {
                // Collect validation errors
                List<string> validationErrors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();

                otpOperationResult.Status = OperationStatus.Failed;
                otpOperationResult.Message = "Validation failed";
                var returnResponse = OperationResultConverter.ConvertTo(otpOperationResult, validationErrors);
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
                var verificationResult = await _otpManager.VerifyOtp(otpCommand.UserId, otpCommand.Otp);
                if (verificationResult.Status == OperationStatus.Success)
                {
                    result.Status = OperationStatus.Success;
                    result.Message = "OTP verified successfully.";
                }
                else
                {
                    result.Status = OperationStatus.Failed;
                    result.Message = verificationResult.Message;  // Message from VerifyOtpAsync
                }

                return ToResponse(result);
            }
            else
            {
                // Collect validation errors
                List<string> validationErrors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();

                result.Status = OperationStatus.Failed;
                result.Message = "Validation failed";
                var returnResponse = OperationResult<List<string>>.ValidationError(data: validationErrors);
                return ToResponse(returnResponse);
            }
        }
    }
}



