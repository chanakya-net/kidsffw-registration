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
    private readonly IRazorPayService _razorPayService;
    private readonly ISalesPartnerService _salesPartnerService;

    public UserRegistrationService(IUnitOfWork unitOfWork, IOtpService otpService, ICouponService couponService, IRazorPayService razorPayService, ISalesPartnerService salesPartnerService)
    {
        _unitOfWork = unitOfWork;
        _otpService = otpService;
        _couponService = couponService;
        _razorPayService = razorPayService;
        _salesPartnerService = salesPartnerService;
    }

    public async Task<CreateUserRegistrationResponseDto> AddUserRegistration(CreateUserRegistrationRequestDto request)
    {
        var result = await _otpService.VerifyOtp(request.MobileNumber, request.OtpCode);
        
        var discount = await _couponService.GetCouponDiscount(request.CouponCode);

        var chargeableAmount = RegistrationFee - (RegistrationFee * (discount / 100));

        var razorPayOrder = _razorPayService.CreateOrder(chargeableAmount * 100);
        
        // TODO: do we need to save order to DB ?
        
         
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
            
            // TODO: sending of the message should be done once payment is verified in webhook callback
            // We have ket it here just for the verification purpose
            
            // if saved successfully then add order id and key to the returned type and return it to the user

            #region Need to move this to webhook callback
            var contact = await _salesPartnerService.GetSalesPartnerContactByCouponId(request.CouponCode);
            if(contact?.ContactNumber is { Length: > 0 })
            {
                var message =
                    $"Hi {contact.Name}, \n  {request.ParentName} has registered successfully using your reference code {request.CouponCode}.";
                await _salesPartnerService.SendRegistrationMessage(contact.ContactNumber, message);
            }
            #endregion
            
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
                Amount = razorPayOrder?.DueAmount,
                OrderId = razorPayOrder?.OrderId,
                RazorPayKey = razorPayOrder?.Key
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