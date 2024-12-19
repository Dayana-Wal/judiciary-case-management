using CaseManagement.Business.Common;
using CaseManagement.DataAccess.Entities;
using CaseManagement.Business.Commands;

namespace CaseManagement.Business.Service
{
    public class OtpManager : BaseManager
    {
        private readonly IOtpCommandHandler _otpCommandHandler;

        public OtpManager(IOtpCommandHandler otpCommandHandler)
        {
            _otpCommandHandler = otpCommandHandler;
        }

        public async Task<Otp> StoreOtp(string userId, string phoneNumber, string usedForCode)
        {
            return await _otpCommandHandler.StoreOtpAsync(userId, phoneNumber, usedForCode);
        }

        public async Task<OperationResult> VerifyOtp(string userId, string otp)
        {
            return await _otpCommandHandler.VerifyOtpAsync(userId, otp);
        }
    }
}
