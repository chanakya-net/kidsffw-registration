namespace kidsffw.Application.Service;

using Common.DTO;
using Common.Interfaces.Repository;
using Domain.Entity;
using Interfaces.Service;
using Specifications;

public class UserRegistrationService : IUserRegistrationService
{
    private const decimal RegistrationFee = 2500;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IOtpService _otpService;
    private readonly ICouponService _couponService;
    private readonly IRazorPayOrderService _razorPayService;

    public UserRegistrationService(IUnitOfWork unitOfWork, IOtpService otpService, ICouponService couponService, IRazorPayOrderService razorPayService)
    {
        _unitOfWork = unitOfWork;
        _otpService = otpService;
        _couponService = couponService;
        _razorPayService = razorPayService;
    }

    public async Task<CreateUserRegistrationResponseDto> AddUserRegistration(CreateUserRegistrationRequestDto request)
    {
        var result = await _otpService.VerifyOtp(request.MobileNumber, request.OtpCode);
        var discount = await _couponService.GetCouponDiscount(request.CouponCode);
        var chargeableAmount = RegistrationFee - (RegistrationFee * (discount / 100));
        var razorPayOrder = _razorPayService.CreateOrder(chargeableAmount * 100,request.ParentName,request.MobileNumber);
        if (!string.IsNullOrEmpty(request.CouponCode) && request.CouponCode.Length > 6)
        {
            request.CouponCode = string.Empty;
        }
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
                        ParentName = request.ParentName,
                        OrderId = razorPayOrder.OrderId,
                        OrderCreationDate = DateTime.UtcNow
                    }
                );
            // Save user and order to DB
            await _unitOfWork.SaveChangesAsync();
            // if saved successfully then add order id and key to the returned type and return it to the user
            return new CreateUserRegistrationResponseDto()
            {
                Id = registeredUser.Id,
                Age = registeredUser.Age,
                Email = registeredUser.Email,
                City = registeredUser.City,
                Gender = registeredUser.Gender,
                KidName = registeredUser.KidName,
                MobileNumber = registeredUser.MobileNumber,
                ParentName = registeredUser.ParentName,
                Amount = razorPayOrder.DueAmount,
                OrderId = razorPayOrder.OrderId,
                RazorPayKey = razorPayOrder.Key,
                CouponCode = registeredUser.CouponCode,
            };
        }
        throw new InvalidOperationException("Invalid otp");
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
                Age = user!.Age,
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

    public async Task<CreateUserRegistrationResponseDto?> GetUserByOrderId(string orderId)
    {
        var spc = Specifications.GetUserByOrderIdr(orderId);
        var result = await _unitOfWork.Repository<UserRegistrationEntity>().FirstOrDefaultAsync(spc);
        if (result != null)
        {
            var fetchedUser = new CreateUserRegistrationResponseDto()
            {
                Age = result.Age,
                Email = result.Email,
                City = result.City,
                Gender = result.Gender,
                KidName = result.KidName,
                Id = result.Id,
                MobileNumber = result.MobileNumber,
                ParentName = result.ParentName,
                OrderId = orderId,
                CouponCode = result.CouponCode
            };
            return fetchedUser;
        }

        return null;
    }
}