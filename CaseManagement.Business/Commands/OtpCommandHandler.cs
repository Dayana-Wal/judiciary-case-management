using CaseManagement.Business.Common;
using CaseManagement.Business.Providers;
using CaseManagement.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace CaseManagement.Business.Commands
{
    public class OtpCommandHandler : IOtpCommandHandler
    {
        private readonly CaseManagementContext _dbContext;
        private readonly OtpProvider _otpProvider;

        public OtpCommandHandler(CaseManagementContext dbContext, OtpProvider otpProvider)
        {
            _dbContext = dbContext;
            _otpProvider = otpProvider;
        }

        public async Task<Otp> StoreOtpAsync(string userId, string phoneNumber, string usedForCode)
        {
            var usedForId = await _dbContext.LookupConstants
                .Where(lc => lc.Code == usedForCode)
                .Select(lc => lc.Id)
                .FirstOrDefaultAsync();

            if (usedForId == 0)
            {
                throw new InvalidOperationException("Invalid UsedForId.");
            }

            var existingOtp = await _dbContext.Otps
                .FirstOrDefaultAsync(o => o.RequestedBy == userId && o.UsedForId == usedForId);

            var (otpValue, otpHash) = OtpProvider.GenerateAndHashOtp();

            if (existingOtp != null)
            {
                if (existingOtp.IsVerified)
                {
                    return existingOtp;
                }

                existingOtp.OtpHash = otpHash;
                existingOtp.GeneratedAt = DateTime.UtcNow;
                existingOtp.ExpiresAt = DateTime.UtcNow.AddMinutes(10);
                existingOtp.IsVerified = false;

                _dbContext.Otps.Update(existingOtp);
            }
            else
            {
                var otp = new Otp
                {
                    Id = BaseManager.NewUlid(),
                    OtpHash = otpHash,
                    IsVerified = false,
                    RequestedBy = userId,
                    UsedForId = usedForId,
                    GeneratedAt = DateTime.UtcNow,
                    ExpiresAt = DateTime.UtcNow.AddMinutes(10)
                };

                await _dbContext.Otps.AddAsync(otp);
            }

            await _dbContext.SaveChangesAsync();
            await _otpProvider.SendOtp(phoneNumber, otpValue);

            return existingOtp ?? new Otp
            {
                OtpHash = otpHash,
                GeneratedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.AddMinutes(10),
                RequestedBy = userId,
                UsedForId = usedForId
            };
        }

        public async Task<OperationResult> VerifyOtpAsync(string userId, string otp)
        {
            if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(otp))
            {
                return new OperationResult
                {
                    Status = OperationStatus.Error,
                    Message = "User ID and OTP cannot be null or empty."
                };
            }

            //Get existed otp from db based on hash value and userId
            var otpExisted = await _dbContext.Otps
                .FirstOrDefaultAsync(o => o.RequestedBy == userId && o.OtpHash == OtpProvider.HashOtp(otp));

            //If otp is not existed
            if(otpExisted == null)
            {
                return new OperationResult
                {
                    Status = OperationStatus.Error,
                    Message = "OTP not found or invalid User ID."
                };
            }

            //Compare OtpHash and otp
            if (otpExisted.OtpHash != OtpProvider.HashOtp(otp))
            {
                return new OperationResult
                {
                    Status = OperationStatus.Error,
                    Message = "Invalid OTP. Verification failed."
                };
            }

            //Check the is otp expired or not
            if (otpExisted.ExpiresAt.HasValue && otpExisted.ExpiresAt.Value < DateTime.UtcNow)
            {
                return new OperationResult
                {
                    Status = OperationStatus.Error,
                    Message = "OTP has expired."
                };
            }

            //Update in db
            otpExisted.IsVerified = true;
            _dbContext.Otps.Update(otpExisted);
            await _dbContext.SaveChangesAsync();

            return new OperationResult
            {
                Status = OperationStatus.Success,
                Message = "OTP verification successful."
            };
        }
    }
}
