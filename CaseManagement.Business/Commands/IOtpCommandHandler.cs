using CaseManagement.Business.Common;
using CaseManagement.DataAccess.Entities;

namespace CaseManagement.Business.Commands
{
    public interface IOtpCommandHandler
    {
        Task<OperationResult<Otp>> StoreOtpAsync(string userId, string phoneNumber, string usedForCode);
        Task<OperationResult<string>> VerifyOtpAsync(string userId, string otp);
    }
}
