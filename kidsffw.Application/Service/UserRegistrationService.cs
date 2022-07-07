namespace kidsffw.Application.Service;

using Common.DTO;
using Common.Interfaces.Repository;
using Domain.Entity;
using Interfaces.Service;
using Specifications;

public class UserRegistrationService : IUserRegistrationService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IOtpService _otpService;

    public UserRegistrationService(IUnitOfWork unitOfWork, IOtpService otpService)
    {
        _unitOfWork = unitOfWork;
        _otpService = otpService;
    }

    public async Task<GetUserRequestDto> AddUserRegistration(CreateUserRegistrationRequestDto request)
    {
        var result = await _otpService.VerifyOtp(request.MobileNumber, request.OtpCode);
        if (result)
        {
            var registeredUser = await _unitOfWork.Repository<UserRegistrationEntity>()
                .AddAsync(
                    new UserRegistrationEntity()
                    {
                        Age = request.Age,
                        Email = request.Email,
                        City = request.City,
                        Gender = request.Gender,
                        CouponCode = request.CouponCode,
                        KidName = request.KidName,
                        MobileNumber = request.MobileNumber,
                        OtpVerified = true,
                        ParentName = request.ParentName
                    }
                );
            await _unitOfWork.SaveChangesAsync();
            return new GetUserRequestDto()
            {
                Id = registeredUser.Id,
                Age = registeredUser.Age,
                Email = registeredUser.Email,
                City = registeredUser.City,
                Gender = registeredUser.Gender,
                KidName = registeredUser.KidName,
                MobileNumber = registeredUser.MobileNumber,
                ParentName = registeredUser.ParentName,
            };
        }

        throw new InvalidOperationException("Invalid otp");
    }

    public async Task<string> UpdateUserRegistration(UpdateUserTransactionDtoRequest request)
    {
        var user = await _unitOfWork.Repository<UserRegistrationEntity>().GetByIdAsync(request.registrationId);
        if (user == null)
        {
            throw new InvalidOperationException("User not found");
        }

        user.TransactionId = request.TransactionId;
        user.TransactionDate = DateTime.UtcNow;
        _unitOfWork.Repository<UserRegistrationEntity>().Update(user);
        await _unitOfWork.SaveChangesAsync();
        return user.TransactionId;
    }

    public async Task<IEnumerable<GetUserRequestDto>> GetUsersByMobileNumber(string mobileNumber)
    {
        var spec = Specifications.GetUserByMobileNumber(mobileNumber);
        var users = await _unitOfWork.Repository<UserRegistrationEntity>().ListAsync(spec);
        var fetchedUserList = new List<GetUserRequestDto>();
        foreach (var user in users)
        {
            var fetchedUser = new GetUserRequestDto()
            {
                Age = user.Age,
                Email = user.Email,
                City = user.City,
                Gender = user.Gender,
                KidName = user.KidName,
                Id = user.Id,
                MobileNumber = user.MobileNumber,
                ParentName = user.ParentName
            };
            fetchedUserList.Add(fetchedUser);
        }
        return fetchedUserList;
    }
}