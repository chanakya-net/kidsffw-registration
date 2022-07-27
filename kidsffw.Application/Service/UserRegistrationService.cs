namespace kidsffw.Application.Service;

using Common.DTO;
using Common.Interfaces.Repository;
using Domain.Entity;
using Interfaces.Service;
using Mapster;
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
        if (!result)
        {
            throw new InvalidOperationException("Invalid otp");
        }
        var discount = await _couponService.GetCouponDiscount(request.CouponCode);
        var chargeableAmount = RegistrationFee - (RegistrationFee * (discount / 100));
        var razorPayOrder = _razorPayService.CreateOrder(chargeableAmount * 100);
        var user = request.Adapt<UserRegistrationEntity>();
        user.OrderId = razorPayOrder.OrderId;
        user.OrderCreationDate = DateTime.UtcNow;
        var registeredUser = await _unitOfWork.Repository<UserRegistrationEntity>()
            .AddAsync(
                user
            );
        // Save user and order to DB
        await _unitOfWork.SaveChangesAsync();
        // if saved successfully then add order id and key to the returned type and return it to the user
        var response = registeredUser.Adapt<CreateUserRegistrationResponseDto>();
        response.Amount = razorPayOrder.DueAmount;
        response.OrderId = razorPayOrder.OrderId;
        response.RazorPayKey = razorPayOrder.Key;
        response.CouponCode = registeredUser.CouponCode;
        return response;
    }
    
    public async Task<IEnumerable<GetUserRequestDto>> GetUsersByMobileNumber(string mobileNumber)
    {
        var spec = Specifications.GetUserByMobileNumber(mobileNumber);
        var users = await _unitOfWork.Repository<UserRegistrationEntity>().ListAsync(spec);
        return users.Adapt<IEnumerable<GetUserRequestDto>>();
    }

    public async Task<CreateUserRegistrationResponseDto?> GetUserByOrderId(string orderId)
    {
        var spc = Specifications.GetUserByOrderIdr(orderId);
        var result = await _unitOfWork.Repository<UserRegistrationEntity>().FirstOrDefaultAsync(spc);
        return result?.Adapt<CreateUserRegistrationResponseDto>();
    }
}