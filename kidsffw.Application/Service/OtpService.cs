namespace kidsffw.Application.Service;

using Common.Interfaces.Repository;
using Domain.Entity;
using Interfaces.Service;
using kidsffw.Application.Specifications;

public class OtpService : IOtpService
{
    private IUnitOfWork _unitOfWork;

    public OtpService(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task<bool> VerifyOtp(string mobileNumber, string otp)
    {
        var spec = Specifications.VerifyOtp(mobileNumber, otp.ToUpper(), DateTime.UtcNow);
        var result = await _unitOfWork.Repository<OtpEntity>().FirstOrDefaultAsync(spec);
        if (result is not null)
        {
            _unitOfWork.Repository<OtpEntity>().Delete(result);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
        return false;
    }
    public async Task<string> CreateOtp(string mobileNumber)
    {
        string otpCode = Path.GetRandomFileName().ToUpper();
        otpCode = otpCode[..6];
        var otpData = new OtpEntity()
        {
            Otp = otpCode, MobileNumber = mobileNumber, ValidTill = DateTime.UtcNow.AddMinutes(3)
        };
        var result = await _unitOfWork.Repository<OtpEntity>().AddAsync(otpData);
        await _unitOfWork.SaveChangesAsync();
        return otpData.Otp;
    }
}