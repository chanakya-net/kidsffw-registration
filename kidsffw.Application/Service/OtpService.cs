namespace kidsffw.Application.Service;

using Common.Interfaces.Repository;
using Interfaces.Service;

public class OtpService : IOtpService
{
    private IUnitOfWork _unitOfWork;

    public OtpService(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public Task<bool> VerifyOtp(string mobileNumber, string otp) => throw new NotImplementedException();

    public Task<bool> SendOtp(string mobileNumber) => throw new NotImplementedException();

    public Task<bool> CreateOtp(string mobileNumber)
    {

    }
}