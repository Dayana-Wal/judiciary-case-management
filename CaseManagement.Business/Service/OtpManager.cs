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

        public async Task<Otp> StoreOtp(string requestedBy, string phoneNumber, string usedForCode)
        {
            return await _otpCommandHandler.StoreOtpAsync(requestedBy, phoneNumber, usedForCode);
        }

        public async Task<OperationResult> VerifyOtp(string userId, string otp)
        {
            return await _otpCommandHandler.VerifyOtpAsync(userId, otp);
        }
    }
}
