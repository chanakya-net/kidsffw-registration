namespace kidsffw.Application.Specifications;

using Domain.Entity;
using kidsffw.Repository.Implementation.Repository;

public static class Specifications
{
    public static BaseSpecification<CouponEntity> GetCouponCodeByCouponId(string couponCode)
    {
        return new BaseSpecification<CouponEntity>(x => x.CouponCode == couponCode);
    }

    public static BaseSpecification<OtpEntity> VerifyOtp(string mobileNumber, string otpCode, DateTime validationTime)
    {
        return new BaseSpecification<OtpEntity>
        (x =>
            x.Otp == otpCode &&
            x.MobileNumber == mobileNumber &&
            x.ValidTill >= validationTime);
    }
    
    public static  BaseSpecification<UserRegistrationEntity> GetUserByMobileNumber(string mobileNumber)
    {
        return new BaseSpecification<UserRegistrationEntity>(x => x.MobileNumber == mobileNumber);
    }
    
    public static  BaseSpecification<CouponEntity> GetPartnerIdByCouponCode(string couponCode)
    {
        return new BaseSpecification<CouponEntity>(x => x.CouponCode == couponCode);
    }
    
    public static BaseSpecification<RazorPayPaymentEntity> GetEventByIdPayment(string eventId)
    {
        return new BaseSpecification<RazorPayPaymentEntity>(x => x.EventId == eventId);
    }

    public static BaseSpecification<RazorPayErrorEntity> GetEventByIdError(string eventId)
    {
        return new BaseSpecification<RazorPayErrorEntity>(x => x.EventId == eventId);
    }
    
    public static  BaseSpecification<UserRegistrationEntity> GetUserByOrderIdr(string orderId)
    {
        return new BaseSpecification<UserRegistrationEntity>(x => x.OrderId == orderId);
    }
}

