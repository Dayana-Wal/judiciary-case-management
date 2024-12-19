using CaseManagement.Business.Common;
using CaseManagement.DataAccess.Entities;

namespace CaseManagement.Business.Commands
{
    public interface IOtpCommandHandler
    {
        Task<Otp> StoreOtpAsync(string requestedBy, string phoneNumber, string usedForCode);
        Task<OperationResult> VerifyOtpAsync(string userId, string otp);
    }
}
