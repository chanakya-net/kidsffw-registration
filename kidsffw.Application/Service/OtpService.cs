namespace kidsffw.Application.Service;

using Common.Interfaces.Repository;
using Domain.Entity;
using Interfaces.Service;
using Specifications;

public class OtpService : IOtpService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMessageService _messageService;

    public OtpService(IUnitOfWork unitOfWork, IMessageService messageService)
    {
        _unitOfWork = unitOfWork;
        _messageService = messageService;
    }

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
        Random generator = new Random();
        string otpCode = generator.Next(0, 1000000).ToString("D6");
        var otpData = new OtpEntity()
        {
            Otp = otpCode, MobileNumber = mobileNumber, ValidTill = DateTime.UtcNow.AddMinutes(3)
        };
        var result = await _unitOfWork.Repository<OtpEntity>().AddAsync(otpData);
        await _unitOfWork.SaveChangesAsync();
        var message = $"Hello, your OTP code for registration is {otpCode}";
        var otpResult = await _messageService.SendMessage(mobileNumber, message);
        if (!otpResult)
        {
            throw new Exception("Otp not sent");
        }
        return otpData.Otp;
    }
}