using kidsffw.Common.DTO;

namespace kidsffw.Application.Interfaces.Service;

public interface ICouponService
{
    Task<CreateCouponResponseDto?> CreateCoupon(CreateCouponRequestDto? request);
    Task<decimal> GetCouponDiscount(string couponCode);
    Task<SalesPartnerContactDto> GetSalesPartnerContactByCouponCode(string couponCode);
    void DisableCoupon(string couponCode);
}