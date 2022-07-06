namespace kidsffw.Application.Interfaces.Service;

public interface IOtpService
{
    public Task<bool> VerifyOtp(string mobileNumber, string otp);
    public Task<string> CreateOtp(string mobileNumber);
}