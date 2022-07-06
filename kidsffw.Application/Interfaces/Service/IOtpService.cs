namespace kidsffw.Application.Interfaces.Service;

public interface IOtpService
{
    public Task<bool> VerifyOtp(string mobileNumber, string otp);
    public Task<bool> SendOtp(string mobileNumber);
    public Task<bool> CreateOtp(string mobileNumber);
}